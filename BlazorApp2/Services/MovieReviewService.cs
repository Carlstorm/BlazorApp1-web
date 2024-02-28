using BlazorApp2.Components;
using BlazorApp2.Models;
using System;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace BlazorApp2.Services
{
    public class MovieReviewService
    {
        public string SearchString { get; set; } = "";
        public int? Genre { get; set; } = null;
        public int? TvGenre { get; set; } = null;
        public string Sort { get; set; } = "popularity.desc";
        public int LastPageLoaded { get; set; } = 0;
        public List<MovieModel> MovieList { get; set; } = new List<MovieModel>();
        public int GalleryMode { get; set; } = 0;
        public bool AllMoviesLoaded { get; set; } = false;
        public async Task<List<MovieModel>>? GetMoviesYes(bool? needReset = null)
        {
            if (needReset != null)
            {
                LastPageLoaded = 0;
                MovieList = new List<MovieModel>();
            }

            Console.WriteLine(Sort);

            int nextPage = LastPageLoaded + 1;
            string baseUrl = "https://api.themoviedb.org/3/";
            string url = "";

            string makeSortString()
            {
                string SortPreflix = "&sort_by=";
                if (GalleryMode == 0)
                {
                    if (Sort == "vote_average.desc")
                        return SortPreflix + Sort + "&vote_count.gte=100";
                    if (Sort == "primary_release_date.desc")
                        return SortPreflix + Sort + "&vote_count.gte=20";
                } else
                {
                    if (Sort == "vote_average.desc")
                        return SortPreflix + Sort + "&vote_count.gte=20";
                    if (Sort == "primary_release_date.desc")
                        return SortPreflix + "first_air_date.desc" + "&vote_count.gte=10";
                }
                return SortPreflix + Sort;
            }

            string makeGenreFilter()
            {
                string GenrePreflix = "&with_genres=";
                if (GalleryMode == 0)
                {
                    if (Genre is null)
                        return "";
                    return GenrePreflix + Genre;
                }
                else
                {
                    if (TvGenre is null)
                        return "";
                    return GenrePreflix + TvGenre;
                }
            }

            if (SearchString != "")
            {
                url = $"{baseUrl}search/{(GalleryMode == 0 ? "movie" : "tv")}?query={SearchString}&include_adult=false&language=en-US&page={nextPage}";
            }
            else if (GalleryMode == 0 && Genre != null || GalleryMode == 1 && TvGenre != null || Sort != "popularity.desc")
            {
                url = $"{baseUrl}discover/{(GalleryMode == 0 ? "movie" : "tv")}?include_adult=false&include_video=false&language=en-US&page={nextPage}{makeSortString()}{makeGenreFilter()}";
            } 
            else
            {
                url = $"{baseUrl}{(GalleryMode == 0 ? "movie" : "tv")}/popular?language=en-US&page={nextPage}";
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(url),
                Headers =
                {
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2MGYyOTA3NjJhMDEyZTI0MWM2YjQzYmZjZTA4NDI4ZiIsInN1YiI6IjY1ZDdkNDQ2ZmNlYzJlMDE4YTJkODBkZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.gsSXfxawAAsdFpXnttCH1JPVXcgq5v_L_zi_rQ_7ORE" },
                    { "accept", "application/json" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<MovieApiResponseModel>(body);

                List<MovieModel> Result = apiResponse?.Results?.Select(movie =>
                {
                    return new MovieModel
                    {
                        Id = movie.Id,
                        ImageUrl = movie.PosterPath,
                        backdropUrl = movie.BackdropPath,
                        Title = movie.Title ?? movie.NameTitle,
                        Date = movie.Date ?? movie.FirstAirDate,
                        Rating = movie.Rating,
                        Overview = movie.Overview,
                    };
                }).ToList() ?? new List<MovieModel>();

                if (Result.Count == 0)
                {
                    AllMoviesLoaded = Result.Count == 0;
                    return MovieList;
                }

                MovieList.AddRange(Result);
                LastPageLoaded++;
                return MovieList;
            }

        }
        public async Task<List<GenreModel>>? InitGenres()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/genre/movie/list?language=en"),
                Headers =
                {
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2MGYyOTA3NjJhMDEyZTI0MWM2YjQzYmZjZTA4NDI4ZiIsInN1YiI6IjY1ZDdkNDQ2ZmNlYzJlMDE4YTJkODBkZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.gsSXfxawAAsdFpXnttCH1JPVXcgq5v_L_zi_rQ_7ORE" },
                    { "accept", "application/json" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<GenreResponseModel>(body);

                List<GenreModel> Result = apiResponse?.Genres?.Select(movie =>
                {
                    return new GenreModel
                    {
                        Id = movie.Id,
                        Name = movie.Name,
                    };
                }).ToList() ?? new List<GenreModel>();

                //if (Result.Count == 0)
                //{
                //    return MovieList;
                //}

                //MovieList.AddRange(Result);
                //LastPageLoaded++;
                return Result;
            }
        }
        public async Task<List<GenreModel>>? InitTvGenres()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/genre/tv/list?language=en"),
                Headers =
                {
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI2MGYyOTA3NjJhMDEyZTI0MWM2YjQzYmZjZTA4NDI4ZiIsInN1YiI6IjY1ZDdkNDQ2ZmNlYzJlMDE4YTJkODBkZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.gsSXfxawAAsdFpXnttCH1JPVXcgq5v_L_zi_rQ_7ORE" },
                    { "accept", "application/json" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                var apiResponse = JsonSerializer.Deserialize<GenreResponseModel>(body);

                List<GenreModel> Result = apiResponse?.Genres?.Select(movie =>
                {
                    return new GenreModel
                    {
                        Id = movie.Id,
                        Name = movie.Name,
                    };
                }).ToList() ?? new List<GenreModel>();

                //if (Result.Count == 0)
                //{
                //    return MovieList;
                //}

                //MovieList.AddRange(Result);
                //LastPageLoaded++;
                return Result;
            }
        }

    }
}


