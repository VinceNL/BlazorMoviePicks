using BlazorMoviePicks.Enums;
using BlazorMoviePicks.Models;

namespace BlazorMoviePicks.Services
{
    public interface IMovieStateService
    {
        MovieType CurrentMovieType { get; }
        List<MovieSummary> SelectedMovies { get; }
        event Action? OnChange;

        Task LoadStateAsync();
        Task SetMovieTypeAsync(MovieType movieType);
        Task AddOrRemoveMovieAsync(MovieSummary movie);
        Task ClearSelectedMoviesAsync();
        Task SortSelectedMoviesAsync(List<MovieSummary> sortedMovies);
    }
}