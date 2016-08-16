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
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents a Meetup member.
    /// </summary>
    public class Member : INotifyPropertyChanged
    {
        private string name = string.Empty;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets member's identifier.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; internal set; }

        /// <summary>
        /// Gets member's name.
        /// </summary>
        [JsonProperty("name")]
        public string Name
        {
            get
            {
                return this.name;
            }

            internal set
            {
                this.name = value;
                this.NotifyPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets country of the member.
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; internal set; }

        /// <summary>
        /// Gets city of the the member.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; internal set; }

        /// <summary>
        /// Gets state of the the member.
        /// </summary>
        [JsonProperty("state")]
        public string State { get; internal set; }

        /// <summary>
        /// Gets a sampling of 50 topics this member has subscribed to.
        /// Only appears if the queried user has not hidden them, or if the authenticated and queried user are the same.
        /// </summary>
        [JsonProperty("topics")]
        public Topic[] Topics { get; internal set; }

        /// <summary>
        /// Gets date and time a member joined in milliseconds since the epoch.
        /// </summary>
        [JsonProperty("joined")]
        public long Joined { get; internal set; }

        /// <summary>
        /// Gets URL to the member's profile page on meetup.com.
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; internal set; }

        /// <summary>
        /// Gets member's photo if available.
        /// </summary>
        [JsonProperty("photo")]
        public Photo Photo { get; internal set; }

        /// <summary>
        /// Gets latitude of the member reported city.
        /// </summary>
        [JsonProperty("lat")]
        public float Latitude { get; internal set; }

        /// <summary>
        /// Gets longitude of the member reported city.
        /// </summary>
        [JsonProperty("lon")]
        public float Longitude { get; internal set; }

        /// <summary>
        /// Gets date and time of member's last activity in milliseconds since the epoch.
        /// </summary>
        [JsonProperty("visited")]
        public long Visited { get; internal set; }

        /// <summary>
        /// Gets details particular to the authorized user. Optional field.
        /// </summary>
        [JsonProperty("self")]
        public Self Self { get; internal set; }

        /// <summary>
        /// Gets member's status. Currently always 'active' for registered members.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; internal set; }

        /// <summary>
        /// Gets member's current language preference.
        /// Returned only when the member in the response matches the authenticated member.
        /// </summary>
        [JsonProperty("lang")]
        public string Language { get; internal set; }

        /// <summary>
        /// Gets third-party services associated with the member account.
        /// </summary>
        [JsonProperty("other_services")]
        public OtherServices OtherServices { get; internal set; }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
