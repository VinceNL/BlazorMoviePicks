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

        public Task<PopularMoviesPagedResponse?>? GetPopularMoviesAsync(int page)
        {
            try
            {
                if (page < 1) page = 1;
                if (page > 500) page = 500;

                return _httpClient.GetFromJsonAsync<PopularMoviesPagedResponse>($"movie/popular?page={page}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public Task<MovieDetails?>? GetMovieDetailsAsync(int id)
        {
            try
            {
                return _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}