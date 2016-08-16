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
    /// A class that represents Metadata.
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Gets identifier.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; internal set; }

        /// <summary>
        /// Gets URL that will request the next set of records.
        /// </summary>
        [JsonProperty("next")]
        public string Next { get; internal set; }

        /// <summary>
        /// Gets API method called.
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; internal set; }

        /// <summary>
        /// Gets total number of records that match your criteria.
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; internal set; }

        /// <summary>
        /// Gets link.
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; internal set; }

        /// <summary>
        /// Gets number of records returned.
        /// </summary>
        [JsonProperty("count")]
        public int Count { get; internal set; }

        /// <summary>
        /// Gets description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// Gets title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; internal set; }

        /// <summary>
        /// Gets URL used to generate this request.
        /// </summary>
        [JsonProperty("url")]
        public string Url { get; internal set; }

        /// <summary>
        /// Gets signed URL used to generate this request.
        /// </summary>
        [JsonProperty("signed_url")]
        public string SignedUrl { get; internal set; }

        /// <summary>
        /// Gets last time one of the items updated in the request.
        /// </summary>
        [JsonProperty("updated")]
        public long Updated { get; internal set; }

        /// <summary>
        /// Gets longitude.
        /// </summary>
        [JsonProperty("lon")]
        public string Longitude { get; internal set; }

        /// <summary>
        /// Gets latitude.
        /// </summary>
        [JsonProperty("lat")]
        public string Latitude { get; internal set; }
    }
}
