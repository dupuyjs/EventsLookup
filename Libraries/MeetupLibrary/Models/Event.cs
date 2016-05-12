using Newtonsoft.Json;

namespace MeetupLibrary.Models
{
    public class Event
    {
        [JsonProperty("utc_offset")]
        public int UtcOffset { get; set; }
        [JsonProperty("venue")]
        public Venue Venue { get; set; }
        [JsonProperty("rsvp_limit")]
        public int RsvpLimit { get; set; }
        [JsonProperty("headcount")]
        public int HeadCount { get; set; }
        [JsonProperty("distance")]
        public double Distance { get; set; }
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        [JsonProperty("waitlist_count")]
        public int WaitlistCount { get; set; }
        [JsonProperty("reated")]
        public object Created { get; set; }
        [JsonProperty("maybe_rsvp_count")]
        public int MaybeRsvpCount { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("event_url")]
        public string EventUrl { get; set; }
        [JsonProperty("yes_rsvp_count")]
        public int YesRsvpCount { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("time")]
        public object Time { get; set; }
        [JsonProperty("updated")]
        public object Updated { get; set; }
        [JsonProperty("group")]
        public Group Group { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("duration")]
        public int? Duration { get; set; }
        [JsonProperty("how_to_find_us")]
        public string HowToFindUs { get; set; }
    }
}
