using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeetupLibrary.Models
{
    public class CitiesResponse
    {
        [JsonProperty("results")]
        public List<City> Results { get; set; }
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}



