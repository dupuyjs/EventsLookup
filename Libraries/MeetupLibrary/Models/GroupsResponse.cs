namespace MeetupLibrary.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents Group response.
    /// </summary>
    public class GroupsResponse
    {
        /// <summary>
        /// Gets list of the Event items.
        /// </summary>
        [JsonProperty("results")]
        public List<Group> Results { get; internal set; }

        /// <summary>
        /// Gets total number of records that match your criteria.
        /// </summary>
        public int TotalCount { get; internal set; }
    }
}
