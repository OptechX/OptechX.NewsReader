using System.Text.Json.Serialization;

namespace OptechX.NewsReader.Models
{
	public class TopHeadline
	{
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }

        [JsonPropertyName("articles")]
        public List<Article>? Articles { get; set; }
    }
}

