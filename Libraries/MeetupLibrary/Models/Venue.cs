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
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents a Meetup venue.
    /// </summary>
    public class Venue
    {
        /// <summary>
        /// Gets venue identifier.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// Gets venue name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets line 1 of venue address.
        /// </summary>
        [JsonProperty("address_1")]
        public string AddressLine1 { get; internal set; }

        /// <summary>
        /// Gets line 2 of venue address.
        /// </summary>
        [JsonProperty("address_2")]
        public string AddressLine2 { get; internal set; }

        /// <summary>
        /// Gets line 3 of venue address.
        /// </summary>
        [JsonProperty("address_3")]
        public string AddressLine3 { get; internal set; }

        /// <summary>
        /// Gets city of venue.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; internal set; }

        /// <summary>
        /// Gets country code of venue.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; internal set; }

        /// <summary>
        /// Gets state of venue (US only).
        /// </summary>
        [JsonProperty("state")]
        public string State { get; internal set; }

        /// <summary>
        /// Gets zip code of venue (US or Canada only).
        /// </summary>
        [JsonProperty("zip")]
        public string Zip { get; internal set; }

        /// <summary>
        /// Gets name of country the city belongs to.
        /// </summary>
        [JsonProperty("localized_country_name")]
        public string CountryName { get; internal set; }

        /// <summary>
        /// Gets longitude of venue.
        /// </summary>
        [JsonProperty("lon")]
        public double Longitude { get; internal set; }

        /// <summary>
        /// Gets latitude of venue.
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether editor altered the original venues pin location.
        /// </summary>
        [JsonProperty("repinned")]
        public bool Repinned { get; internal set; }
    }
}