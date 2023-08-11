using System.Text.Json.Serialization;

namespace OptechX.NewsReader.Models.OptechX.API.Generic
{
	public class OptechXNewsItem
	{
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("heading")]
        public string? Heading { get; set; }

        [JsonPropertyName("preview")]
        public string? Preview { get; set; }

        [JsonPropertyName("link")]
        public string? Link { get; set; }
    }
}

