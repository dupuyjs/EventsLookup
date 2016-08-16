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
    /// A class that represents a Meetup topic.
    /// </summary>
    public class Topic
    {
        /// <summary>
        /// Gets numeric topic id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// Gets display name of the topic.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        /// Gets unique keyword used to identify this topic.
        /// </summary>
        [JsonProperty("urlkey")]
        public string UrlKey { get; internal set; }

        /// <summary>
        /// Gets number of groups using this topic.
        /// </summary>
        [JsonProperty("group_count")]
        public int GroupCount { get; internal set; }

        /// <summary>
        /// Gets number of members interested in this topic.
        /// </summary>
        [JsonProperty("member_count")]
        public int MemberCount { get; internal set; }

        /// <summary>
        /// Gets description of the topic.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; internal set; }

        /// <summary>
        /// Gets language topic originates from.
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; internal set; }
    }
}
