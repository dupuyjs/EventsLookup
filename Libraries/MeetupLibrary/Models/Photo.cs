﻿// ******************************************************************
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
    /// A class that represents a photo.
    /// </summary>
    public class Photo
    {
        /// <summary>
        /// Gets photo identifier.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// Gets URL for the photo at its maximum size.
        /// </summary>
        [JsonProperty("highres_link")]
        public string HighresLink { get; internal set; }

        /// <summary>
        /// Gets URL for a standard size of the photo.
        /// </summary>
        [JsonProperty("photo_link")]
        public string PhotoLink { get; internal set; }

        /// <summary>
        /// Gets URL for a thumbnail of the photo.
        /// </summary>
        [JsonProperty("thumb_link")]
        public string ThumbLink { get; internal set; }
    }
}
