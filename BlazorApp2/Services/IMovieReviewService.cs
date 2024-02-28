using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public interface IMovieReviewService
    {
        Task<List<GenreModel>>? InitGenres();

        Task<List<GenreModel>>? InitTvGenres();

        Task<List<MovieModel>>? GetMoviesYes(bool? needReset);

        List<MovieModel> MovieList { get; set; }

        string SearchString { get; set; }
        int LastPageLoaded { get; set; }
        int? Genre { get; set; }
        int? TvGenre { get; set; }
        string Sort { get; set; }
        int GalleryMode { get; set; }

        bool AllMoviesLoaded { get; set; }
    }
}
