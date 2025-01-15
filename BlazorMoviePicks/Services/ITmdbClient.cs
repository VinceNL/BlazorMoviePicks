using BlazorMoviePicks.Models;

namespace BlazorMoviePicks.Services
{
    public interface ITmdbClient
    {
        Task<MoviesPagedResponse?> GetPopularMoviesAsync(int page);
        Task<MoviesPagedResponse?> GetTopRatedMoviesAsync(int page);
        Task<MovieDetails?> GetMovieDetailsAsync(int id);
    }
}