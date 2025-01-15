using BlazorMoviePicks.Models;

namespace BlazorMoviePicks.Services
{
    public interface ITmdbClient
    {
        Task<PopularMoviesPagedResponse?> GetPopularMoviesAsync(int page);
        Task<PopularMoviesPagedResponse?> GetTopRatedMoviesAsync(int page);
        Task<MovieDetails?> GetMovieDetailsAsync(int id);
    }
}