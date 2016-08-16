namespace MeetupLibrary.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents Cities response.
    /// </summary>
    public class CitiesResponse
    {
        /// <summary>
        /// Gets list of the City items.
        /// </summary>
        [JsonProperty("results")]
        public List<City> Results { get; internal set; }

        /// <summary>
        /// Gets additional information about the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }
    }
}