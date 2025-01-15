using System.Text.Json.Serialization;

namespace BlazorMoviePicks.Models
{
    public class MoviesPagedResponse
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public IEnumerable<MovieResponse> Results { get; set; } = [];

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total_results")]
        public int TotalResults { get; set; }
    }
}