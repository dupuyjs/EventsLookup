using Newtonsoft.Json;

namespace MeetupLibrary.Models
{
    public class Category
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("sort_name")]
        public string SortName { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("shortname")]
        public string ShortName { get; set; }
    }
}
