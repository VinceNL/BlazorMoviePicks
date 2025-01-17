using Blazored.LocalStorage;
using BlazorMoviePicks.Enums;
using BlazorMoviePicks.Models;
using BlazorMoviePicks.Services;
using Moq;
using System.Text.Json;

namespace BlazorMovieTests
{
    public class MovieStateServiceTests
    {
        private readonly Mock<ILocalStorageService> _localStorageMock;
        private readonly IMovieStateService _movieStateService;

        public MovieStateServiceTests()
        {
            _localStorageMock = new Mock<ILocalStorageService>();
            _movieStateService = new MovieStateService(_localStorageMock.Object);
        }

        [Fact]
        public async Task LoadStateAsync_LoadsStateFromLocalStorage()
        {
            // Arrange
            var movieTypeString = "TopRated";
            var selectedMoviesJson = JsonSerializer.Serialize(new List<MovieSummary>
            {
                new MovieSummary { Id = 1, Title = "Movie 1" },
                new MovieSummary { Id = 2, Title = "Movie 2" }
            });

            _localStorageMock.Setup(ls => ls.GetItemAsync<string>("currentMovieType", CancellationToken.None))
                .ReturnsAsync(movieTypeString);
            _localStorageMock.Setup(ls => ls.GetItemAsync<string>("selectedMovieSummaries", CancellationToken.None))
                .ReturnsAsync(selectedMoviesJson);

            // Act
            await _movieStateService.LoadStateAsync();

            // Assert
            Assert.Equal(MovieType.TopRated, _movieStateService.CurrentMovieType);
            Assert.Equal(2, _movieStateService.SelectedMovies.Count);
            Assert.Equal("Movie 1", _movieStateService.SelectedMovies[0].Title);
            Assert.Equal("Movie 2", _movieStateService.SelectedMovies[1].Title);
        }

        [Fact]
        public async Task SetMovieTypeAsync_SetsMovieTypeAndSavesToLocalStorage()
        {
            // Arrange
            var movieType = MovieType.TopRated;

            // Act
            await _movieStateService.SetMovieTypeAsync(movieType);

            // Assert
            Assert.Equal(movieType, _movieStateService.CurrentMovieType);
            _localStorageMock.Verify(ls => ls.SetItemAsync("currentMovieType", movieType.ToString(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task AddOrRemoveMovieAsync_AddsMovieIfNotExists()
        {
            // Arrange
            var movie = new MovieSummary { Id = 1, Title = "Movie 1" };

            // Act
            await _movieStateService.AddOrRemoveMovieAsync(movie);

            // Assert
            Assert.Single(_movieStateService.SelectedMovies);
            Assert.Equal(movie, _movieStateService.SelectedMovies[0]);
            _localStorageMock.Verify(ls => ls.SetItemAsync("selectedMovieSummaries", It.IsAny<string>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task AddOrRemoveMovieAsync_RemovesMovieIfExists()
        {
            // Arrange
            var movie = new MovieSummary { Id = 1, Title = "Movie 1" };
            _movieStateService.SelectedMovies.Add(movie);

            // Act
            await _movieStateService.AddOrRemoveMovieAsync(movie);

            // Assert
            Assert.Empty(_movieStateService.SelectedMovies);
            _localStorageMock.Verify(ls => ls.SetItemAsync("selectedMovieSummaries", It.IsAny<string>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task ClearSelectedMoviesAsync_ClearsSelectedMoviesAndRemovesFromLocalStorage()
        {
            // Arrange
            _movieStateService.SelectedMovies.Add(new MovieSummary { Id = 1, Title = "Movie 1" });

            // Act
            await _movieStateService.ClearSelectedMoviesAsync();

            // Assert
            Assert.Empty(_movieStateService.SelectedMovies);
            _localStorageMock.Verify(ls => ls.RemoveItemAsync("selectedMovieSummaries", CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task SortSelectedMoviesAsync_SortsMoviesAndSavesToLocalStorage()
        {
            // Arrange
            var sortedMovies = new List<MovieSummary>
            {
                new MovieSummary { Id = 2, Title = "Movie 2" },
                new MovieSummary { Id = 1, Title = "Movie 1" }
            };

            // Act
            await _movieStateService.SortSelectedMoviesAsync(sortedMovies);

            // Assert
            Assert.Equal(sortedMovies, _movieStateService.SelectedMovies);
            _localStorageMock.Verify(ls => ls.SetItemAsync("selectedMovieSummaries", It.IsAny<string>(), CancellationToken.None), Times.Once);
        }
    }
}