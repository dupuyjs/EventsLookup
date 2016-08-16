// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace MeetupLibrary.Models
{
    using System;
    using MeetupLibrary.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents a Meetup event.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets event identifier. May be numeric or alphanumeric, always served as a string.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets name of the event.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets description of the event.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// Gets venue.
        /// </summary>
        [JsonProperty("venue")]
        public Venue Venue { get; internal set; }

        /// <summary>
        /// Gets number of members in attendance according to the attendance taker. This may be 0 if attendance has not yet been taken.
        /// </summary>
        [JsonProperty("headcount")]
        public int HeadCount { get; internal set; }

        /// <summary>
        /// Gets distance in miles from the search location, if one was specified.
        /// </summary>
        [JsonProperty("distance")]
        public double Distance { get; internal set; }

        /// <summary>
        /// Gets Event visibility: "public", "members" or "public_limited".
        /// Events in private groups that do not expose limited information are visible only to that group's members and should not be made public.
        /// </summary>
        [JsonProperty("visibility")]
        public string Visibility { get; internal set; }

        /// <summary>
        /// Gets number of yes RSVPs an event can have before members will be added to the waiting list.
        /// </summary>
        [JsonProperty("rsvp_limit")]
        public int RsvpLimit { get; internal set; }

        /// <summary>
        /// Gets number of yes RSVPs including guests.
        /// </summary>
        [JsonProperty("yes_rsvp_count")]
        public int YesRsvpCount { get; internal set; }

        /// <summary>
        /// Gets number of maybe RSVPs including guests.
        /// </summary>
        [JsonProperty("maybe_rsvp_count")]
        public int MaybeRsvpCount { get; internal set; }

        /// <summary>
        /// Gets URL of the event's page on meetup.com.
        /// </summary>
        [JsonProperty("event_url")]
        public string EventUrl { get; internal set; }

        /// <summary>
        /// Gets URL of the event photo, if one exists.
        /// </summary>
        [JsonProperty("photo_url")]
        public int PhotoUrl { get; internal set; }

        /// <summary>
        /// Gets UTC creation time of the event, in milliseconds since the epoch.
        /// </summary>
        [JsonProperty("created")]
        public object Created { get; internal set; }

        /// <summary>
        /// Gets UTC start time of the event, in milliseconds since the epoch.
        /// </summary>
        [JsonProperty("time")]
        [JsonConverter(typeof(MicrosecondEpochConverter))]
        public DateTime Time { get; internal set; }

        /// <summary>
        /// Gets local offset from UTC time, in milliseconds.
        /// </summary>
        [JsonProperty("utc_offset")]
        public int UtcOffset { get; internal set; }

        /// <summary>
        /// Gets UTC last modified time of the event, in milliseconds since the epoch.
        /// </summary>
        [JsonProperty("updated")]
        public object Updated { get; internal set; }

        /// <summary>
        /// Gets group that is hosting the event/
        /// </summary>
        [JsonProperty("group")]
        public Group Group { get; internal set; }

        /// <summary>
        /// Gets status. "cancelled", "upcoming", "past", "proposed", "suggested" or "draft".
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; internal set; }

        /// <summary>
        /// Gets Event duration in milliseconds, if an end time is specified by the organizer.
        /// When not present, a default of 3 hours may be assumed by applications.
        /// </summary>
        [JsonProperty("duration")]
        public int? Duration { get; internal set; }

        /// <summary>
        /// Gets information provided by the event host for "How will members find you there?".
        /// Visible when location is visible to the authenticated member.
        /// </summary>
        [JsonProperty("how_to_find_us")]
        public string HowToFindUs { get; internal set; }

        /// <summary>
        /// Gets start time of the event; with local offset from UTC time.
        /// </summary>
        public DateTime TimeWithOffset
        {
            get
            {
                return this.Time.AddTicks(this.UtcOffset);
            }
        }
    }
}