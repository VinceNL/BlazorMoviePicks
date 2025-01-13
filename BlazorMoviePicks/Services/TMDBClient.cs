using BlazorMoviePicks.Models;
using System.Net.Http.Json;

namespace BlazorMoviePicks.Services
{
    public class TmdbClient : ITmdbClient
    {
        private readonly HttpClient _httpClient;

        public TmdbClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;

            string baseUrl = config["BaseUrl"] ?? throw new KeyNotFoundException("BaseUrl not found");
            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = config["TMDBKey"] ?? throw new KeyNotFoundException("TMDBKey not found");
            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        }

        public async Task<PopularMoviesPagedResponse?> GetPopularMoviesAsync(int page)
        {
            try
            {
                if (page < 1) page = 1;
                if (page > 500) page = 500;

                return await _httpClient.GetFromJsonAsync<PopularMoviesPagedResponse>($"movie/popular?page={page}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<MovieDetails?> GetMovieDetailsAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MovieDetails>($"movie/{id}");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}