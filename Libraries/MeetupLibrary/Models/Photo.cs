using Newtonsoft.Json;

namespace MeetupLibrary.Models
{
    public class Photo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("highres_link")]
        public string HighresLink { get; set; }
        [JsonProperty("photo_link")]
        public string PhotoLink { get; set; }
        [JsonProperty("thumb_link")]
        public string ThumbLink { get; set; }
    }
}
