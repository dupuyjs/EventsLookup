using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupLibrary.Models
{
    public class Venue
    {
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("localized_country_name")]
        public string CountryName { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("address_1")]
        public string Address { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("repinned")]
        public bool Repinned { get; set; }
    }
}
