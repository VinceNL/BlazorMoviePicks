namespace BlazorMoviePicks.Models
{
    public class MovieSummary
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public float VoteAverage { get; set; }

        public string? OriginalTitle { get; set; }
    }
}