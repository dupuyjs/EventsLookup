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
    /// A class that represents a meetup city.
    /// </summary>
    public class City
    {
        /// <summary>
        /// Gets numeric identifier of the city.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// Gets name of the city.
        /// </summary>
        [JsonProperty("city")]
        public string CityName { get; internal set; }

        /// <summary>
        /// Gets ISO_3166-1 country code for the country which contains the city.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; internal set; }

        /// <summary>
        /// Gets zip code of the city. For cities in countries without ZIP codes, a placeholder will be returned.
        /// </summary>
        [JsonProperty("zip")]
        public string Zip { get; internal set; }

        /// <summary>
        /// Gets name of the country which contains the city.
        /// </summary>
        [JsonProperty("localized_country_name")]
        public string CountryName { get; internal set; }

        /// <summary>
        /// Gets distance away from the provided coordinates, if applicable.
        /// </summary>
        [JsonProperty("distance")]
        public double Distance { get; internal set; }

        /// <summary>
        /// Gets longitude of the city.
        /// </summary>
        [JsonProperty("lon")]
        public double Longitude { get; internal set; }

        /// <summary>
        /// Gets latitude of the city.
        /// </summary>
        [JsonProperty("lat")]
        public double Latitude { get; internal set; }

        /// <summary>
        /// Gets the best-match ranking of this result.
        /// </summary>
        [JsonProperty("ranking")]
        public int Ranking { get; internal set; }

        /// <summary>
        /// Gets number of Meetup members in the city.
        /// </summary>
        [JsonProperty("member_count")]
        public int MemberCount { get; internal set; }
    }
}
