using Newtonsoft.Json;

namespace MeetupLibrary.Models
{
    public class Topic
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("urlkey")]
        public string UrlKey { get; set; }
        [JsonProperty("group_count")]
        public int GroupCount { get; set; }
        [JsonProperty("member_count")]
        public int MemberCount { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("lang")]
        public string Language { get; set; }
    }
}
