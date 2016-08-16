namespace MeetupLibrary.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents Events response.
    /// </summary>
    public class EventsResponse
    {
        /// <summary>
        /// Gets list of the Event items.
        /// </summary>
        [JsonProperty("results")]
        public List<Event> Results { get; internal set; }

        /// <summary>
        /// Gets additional information about the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }
    }
}
