using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeetupLibrary.Models
{
    public class EventsResponse
    {
        [JsonProperty("results")]
        public List<Event> Results { get; set; }
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
