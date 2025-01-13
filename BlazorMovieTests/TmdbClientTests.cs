using BlazorMoviePicks.Models;
using BlazorMoviePicks.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;

namespace BlazorMovieTests
{
    public class TmdbClientTests
    {
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly HttpClient _httpClient;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly ITmdbClient _tmdbClient;

        public TmdbClientTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.themoviedb.org/3/")
            };

            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(config => config["BaseUrl"]).Returns("https://api.themoviedb.org/3/");
            _configurationMock.Setup(config => config["TMDBKey"]).Returns("your_api_key_here");

            _tmdbClient = new TmdbClient(_httpClient, _configurationMock.Object);
        }

        [Fact]
        public async Task GetPopularMoviesAsync_ReturnsPopularMovies()
        {
            // Arrange
            var responseContent = new PopularMoviesPagedResponse
            {
                Page = 1,
                Results = new List<PopularMovie>
            {
                new PopularMovie { Id = 1, Title = "Movie 1" },
                new PopularMovie { Id = 2, Title = "Movie 2" }
            },
                TotalPages = 1,
                TotalResults = 2
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseContent)
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _tmdbClient.GetPopularMoviesAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Page);
            Assert.Equal(2, result.Results.Count());
        }

        [Fact]
        public async Task GetMovieDetailsAsync_ReturnsMovieDetails()
        {
            // Arrange
            var responseContent = new MovieDetails
            {
                Id = 1,
                Title = "Movie 1"
            };

            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(responseContent)
            };

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);

            // Act
            var result = await _tmdbClient.GetMovieDetailsAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Movie 1", result.Title);
        }

        [Fact]
        public async Task GetPopularMoviesAsync_HandlesHttpRequestException()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            // Act
            var result = await _tmdbClient.GetPopularMoviesAsync(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetMovieDetailsAsync_HandlesHttpRequestException()
        {
            // Arrange
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ThrowsAsync(new HttpRequestException("Network error"));

            // Act
            var result = await _tmdbClient.GetMovieDetailsAsync(1);

            // Assert
            Assert.Null(result);
        }
    }

}