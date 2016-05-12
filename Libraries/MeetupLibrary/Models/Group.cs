using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupLibrary.Models
{
    public class Group
    {
        [JsonProperty("score")]
        public double Score { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("link")]
        public string Link { get; set; }
        [JsonProperty("urlname")]
        public string UrlName { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("created")]
        public object Created { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        [JsonProperty("localized_country_name")]
        public string LocalizedCountryName { get; set; }
        [JsonProperty("state")]
        public string State { get; set; }
        [JsonProperty("join_mode")]
        public string JoinMode { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lon")]
        public double Longitude { get; set; }
        [JsonProperty("members")]
        public int Members { get; set; }
        [JsonProperty("organizer")]
        public Organizer Organizer { get; set; }
        [JsonProperty("who")]
        public string Who { get; set; }
        [JsonProperty("group_photo")]
        public Photo GroupPhoto { get; set; }
        [JsonProperty("key_photo")]
        public Photo KeyPhoto { get; set; }
        [JsonProperty("timezone")]
        public string TimeZone { get; set; }
        [JsonProperty("next_event")]
        public Event NextEvent { get; set; }
        [JsonProperty("category")]
        public Category Category { get; set; }
        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }
    }
}
