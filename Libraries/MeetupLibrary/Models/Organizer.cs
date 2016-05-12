using Newtonsoft.Json;

namespace MeetupLibrary.Models
{
    public class Organizer
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("photo")]
        public Photo Photo { get; set; }
    }
}
