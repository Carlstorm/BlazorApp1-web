using System.Text.Json.Serialization;

namespace BlazorApp2.Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? backdropUrl { get; set; }
        public double? Rating { get; set; }
        public string? Date { get; set; }
        public string? Overview { get; set; }
        public string? FirstAirDate { get; set; }
        public string? NameTitle { get; set; }
    }

    public class ApiMovieModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string BackdropPath { get; set; }
        [JsonPropertyName("overview")]
        public string Overview { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set;}

        [JsonPropertyName("vote_average")]
        public double Rating { get; set; }

        [JsonPropertyName("release_date")]
        public string Date { get; set; }

        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }

        [JsonPropertyName("name")]
        public string NameTitle { get; set; }
    }

    public class MovieApiResponseModel
    {
        [JsonPropertyName("results")]
        public List<ApiMovieModel> Results { get; set; }
    }

    public class GenreResponseModel
    {
        [JsonPropertyName("genres")]
        public List<GenreModel>? Genres { get; set; }
    }
    public class GenreModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
    public class MovieDetails
    {

    }
}
