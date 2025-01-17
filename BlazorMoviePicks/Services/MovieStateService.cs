using Blazored.LocalStorage;
using BlazorMoviePicks.Enums;
using BlazorMoviePicks.Models;
using System.Text.Json;

namespace BlazorMoviePicks.Services
{
    public class MovieStateService(ILocalStorageService localStorage) : IMovieStateService
    {
        public MovieType CurrentMovieType { get; private set; } = MovieType.Popular;
        public List<MovieSummary> SelectedMovies { get; private set; } = new List<MovieSummary>();

        public event Action? OnChange;

        public async Task LoadStateAsync()
        {
            var movieTypeString = await localStorage.GetItemAsync<string>("currentMovieType");
            if (!string.IsNullOrEmpty(movieTypeString) && Enum.TryParse<MovieType>(movieTypeString, out var movieType))
            {
                CurrentMovieType = movieType;
            }

            var json = await localStorage.GetItemAsync<string>("selectedMovieSummaries");
            if (!string.IsNullOrEmpty(json))
            {
                SelectedMovies = JsonSerializer.Deserialize<List<MovieSummary>>(json)!;
            }

            NotifyStateChanged();
        }

        public async Task SetMovieTypeAsync(MovieType movieType)
        {
            CurrentMovieType = movieType;
            await localStorage.SetItemAsync("currentMovieType", movieType.ToString());
            NotifyStateChanged();
        }

        public async Task AddOrRemoveMovieAsync(MovieSummary movie)
        {
            var existing = SelectedMovies.FirstOrDefault(sm => sm.Id == movie.Id);
            if (existing != null)
            {
                SelectedMovies.Remove(existing);
            }
            else
            {
                SelectedMovies.Add(movie);
            }

            var json = JsonSerializer.Serialize(SelectedMovies);
            await localStorage.SetItemAsync("selectedMovieSummaries", json);
            NotifyStateChanged();
        }

        public async Task ClearSelectedMoviesAsync()
        {
            SelectedMovies.Clear();
            await localStorage.RemoveItemAsync("selectedMovieSummaries");
            NotifyStateChanged();
        }

        public async Task SortSelectedMoviesAsync(List<MovieSummary> sortedMovies)
        {
            SelectedMovies = sortedMovies;
            var json = JsonSerializer.Serialize(SelectedMovies);
            await localStorage.SetItemAsync("selectedMovieSummaries", json);
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}