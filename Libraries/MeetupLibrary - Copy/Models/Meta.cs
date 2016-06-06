using Newtonsoft.Json;

namespace MeetupLibrary.Models
{
    public class Meta
    {
        [JsonProperty("next")]
        public string Next { get; set; }
        [JsonProperty("method")]
        public string Method { get; set; }
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("lon")]
        public string Longitude { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("signed_url")]
        public string SignedUrl { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("updated")]
        public long Updated { get; set; }
        [JsonProperty("lat")]
        public string Latitude { get; set; }
    }
}
