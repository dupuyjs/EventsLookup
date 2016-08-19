// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace MeetupLibrary
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MeetupLibrary.Models;

    /// <summary>
    /// Interface which exposes methods to call Meetup platform.
    /// </summary>
    public interface IMeetupClient
    {
        /// <summary>
        /// Returns Meetup group categories.
        /// </summary>
        /// <returns>A <see cref="CategoriesResponse"/> object.</returns>
        Task<CategoriesResponse> GetCategoriesAsync();

        /// <summary>
        /// Returns Meetup cities.
        /// </summary>
        /// <param name="country">A valid two character country code, defaults to fr.</param>
        /// <returns>A <see cref="CitiesResponse"/> object.</returns>
        Task<CitiesResponse> GetCitiesAsync(string country = "fr");

        /// <summary>
        /// Returns Meetup groups.
        /// </summary>
        /// <param name="topicId">Comma-delimited list of numeric topic ids.</param>
        /// <param name="zip">Zipcode of location to limit search to.</param>
        /// <param name="category">Comma-delimited list of numeric category ids.</param>
        /// <param name="upcomingOnly">If true, filters text and category based searches on groups that have upcoming events. Defaults to false.</param>
        /// <param name="ordering">An <see cref="OrderingEnum"/> enumeration value.</param>
        /// <param name="country">A valid two character country code, defaults to fr.</param>
        /// <returns>A <see cref="GroupsResponse"/> object.</returns>
        Task<GroupsResponse> GetGroupsAsync(int topicId, string zip, int? category, bool upcomingOnly, OrderingEnum ordering = OrderingEnum.MostActive, string country = "fr");

        /// <summary>
        /// Returns Meetup events.
        /// </summary>
        /// <param name="groupId">Group hosting the event.</param>
        /// <returns>An <see cref="EventsResponse"/> object.</returns>
        Task<EventsResponse> GetEventsAsync(int groupId);

        /// <summary>
        /// Returns Meetup topics. Recommends suggestions for group topics.
        /// </summary>
        /// <param name="query">Free form text search.</param>
        /// <param name="lang">Defines a language preference for ordering results.</param>
        /// <returns>List of <see cref="Topic"/> objects.</returns>
        Task<List<Topic>> GetTopicsAsync(string query, string lang = "en-US");

        /// <summary>
        /// Returns current user profile.
        /// </summary>
        /// <returns><see cref="Member"/> object.</returns>
        Task<Member> GetUserProfileAsync();

        /// <summary>
        /// Returns Meetup events. Filters events by the currently authenticated member's RSVP status.
        /// </summary>
        /// <param name="memberId">Single member id, to find events in this member's groups.</param>
        /// <returns>An <see cref="EventsResponse"/> object.</returns>
        Task<EventsResponse> GetUserCalendarAsync(int memberId);
    }
}
