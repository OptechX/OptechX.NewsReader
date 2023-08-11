using System.Text.Json.Serialization;

namespace OptechX.NewsReader.Models
{
	public class SavedArticle
	{
        [JsonPropertyName("heading")]
        public string? Title { get; set; }

        [JsonPropertyName("preview")]
        public string? Description { get; set; }

        [JsonPropertyName("link")]
        public string? Url { get; set; }

        [JsonPropertyName("image")]
        public string? UrlToImage { get; set; }
    }
}

