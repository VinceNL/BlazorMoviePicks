using BlazorMoviePicks.Models;
using System.Net.Http.Json;

namespace BlazorMoviePicks.Services
{
    public class TMDBClient
    {
        private readonly HttpClient _httpClient;

        public TMDBClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");
            _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = config["TMDBKey"] ?? throw new Exception("TMDBKey not found");
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        }

        public Task<PopularMoviesPagedResponse?> GetPopularMoviesAsync()
        {
            return _httpClient.GetFromJsonAsync<PopularMoviesPagedResponse>("movie/popular");
        }

        public Task<MovieDetails?> GetMovieDetailsAsync(int id)
        {
            return _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
        }
    }
}