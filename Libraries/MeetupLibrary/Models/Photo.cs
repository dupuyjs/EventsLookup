using Newtonsoft.Json;

namespace MeetupLibrary.Models
{
    public class Photo
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("highres_link")]
        public string highres_link { get; set; }
        [JsonProperty("photo_link")]
        public string photo_link { get; set; }
        [JsonProperty("thumb_link")]
        public string thumb_link { get; set; }
    }
}
