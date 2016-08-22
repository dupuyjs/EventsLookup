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
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MeetupLibrary.Helpers;
    using MeetupLibrary.Models;
    using OAuth;

    /// <summary>
    /// Core Meetup platform class. It exposes methods to call Meetup platform.
    /// </summary>
    public sealed class MeetupClient : SimpleServiceClient, IMeetupClient
    {
        private Uri _hostUri = new Uri("https://api.meetup.com");

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetupClient"/> class.
        /// </summary>
        internal MeetupClient()
        {
        }

        /// <summary>
        /// Returns Meetup group categories.
        /// </summary>
        /// <returns>A <see cref="CategoriesResponse"/> object.</returns>
        public async Task<CategoriesResponse> GetCategoriesAsync()
        {
            var accessToken = await MeetupOAuthService.Instance.GetAccessToken();
            var parameters = new Dictionary<string, string>();
            parameters.Add("token", accessToken);

            var template = new UriTemplate("/2/categories?access_token={token}");

            return await GetWithRetryAsync<CategoriesResponse>(_hostUri, template, parameters);
        }

        /// <summary>
        /// Returns Meetup cities.
        /// </summary>
        /// <param name="country">A valid two character country code, defaults to fr.</param>
        /// <returns>A <see cref="CitiesResponse"/> object.</returns>
        public async Task<CitiesResponse> GetCitiesAsync(string country = "fr")
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("country", country.ToString());

            var template = new UriTemplate("/2/cities?country={country}&order=size&page=50");

            return await GetWithRetryAsync<CitiesResponse>(_hostUri, template, parameters);
        }

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
        public async Task<GroupsResponse> GetGroupsAsync(int topicId, string zip, int? category, bool upcomingOnly, OrderingEnum ordering = OrderingEnum.MostActive, string country = "fr")
        {
            var accessToken = await MeetupOAuthService.Instance.GetAccessToken();
            var parameters = new Dictionary<string, string>();
            parameters.Add("token", accessToken);
            parameters.Add("topicid", topicId.ToString());

            if (!string.IsNullOrWhiteSpace(zip))
            {
                parameters.Add("zip", zip);
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                parameters.Add("country", country);
            }

            if (category.HasValue)
            {
                parameters.Add("category", category.ToString());
            }

            parameters.Add("upcomingonly", upcomingOnly.ToString());
            parameters.Add("ordering", ordering.ToFriendlyString());

            var template = new UriTemplate("/find/groups?radius=50&topic_id={topicid}&zip={zip}&category={category}&order={ordering}&upcoming_events={upcomingonly}&country={country}&page=50&access_token={token}");

            return await GetWithRetryAsync<GroupsResponse>(_hostUri, template, parameters);
        }

        /// <summary>
        /// Returns Meetup events.
        /// </summary>
        /// <param name="groupId">Group hosting the event.</param>
        /// <returns>An <see cref="EventsResponse"/> object.</returns>
        public async Task<EventsResponse> GetEventsAsync(int groupId)
        {
            var accessToken = await MeetupOAuthService.Instance.GetAccessToken();
            var parameters = new Dictionary<string, string>();
            parameters.Add("token", accessToken);
            parameters.Add("groupId", groupId.ToString());

            var template = new UriTemplate("/2/events?group_id={groupId}&access_token={token}");

            return await GetWithRetryAsync<EventsResponse>(_hostUri, template, parameters);
        }

        /// <summary>
        /// Returns Meetup topics. Recommends suggestions for group topics.
        /// </summary>
        /// <param name="query">Free form text search.</param>
        /// <param name="lang">Defines a language preference for ordering results.</param>
        /// <returns>List of <see cref="Topic"/> objects.</returns>
        public async Task<List<Topic>> GetTopicsAsync(string query, string lang = "en-US")
        {
            var accessToken = await MeetupOAuthService.Instance.GetAccessToken();
            var parameters = new Dictionary<string, string>();
            parameters.Add("token", accessToken);

            if (!string.IsNullOrWhiteSpace(query))
            {
                parameters.Add("query", Uri.EscapeDataString(query));
            }

            if (!string.IsNullOrEmpty(lang))
            {
                parameters.Add("lang", lang);
            }

            var template = new UriTemplate("/recommended/group_topics?text={query}&lang={lang}&access_token={token}");

            return await GetWithRetryAsync<List<Topic>>(_hostUri, template, parameters);
        }

        /// <summary>
        /// Returns current user profile.
        /// </summary>
        /// <returns><see cref="Member"/> object.</returns>
        public async Task<Member> GetUserProfileAsync()
        {
            var accessToken = await MeetupOAuthService.Instance.GetAccessToken();
            var parameters = new Dictionary<string, string>();
            parameters.Add("token", accessToken);

            var template = new UriTemplate("/2/member/self?access_token={token}");

            return await GetWithRetryAsync<Member>(_hostUri, template, parameters);
        }

        /// <summary>
        /// Returns Meetup events. Filters events by the currently authenticated member's RSVP status.
        /// </summary>
        /// <param name="memberId">Single member id, to find events in this member's groups.</param>
        /// <returns>An <see cref="EventsResponse"/> object.</returns>
        public async Task<EventsResponse> GetUserCalendarAsync(int memberId)
        {
            var accessToken = await MeetupOAuthService.Instance.GetAccessToken();
            var parameters = new Dictionary<string, string>();
            parameters.Add("token", accessToken);
            parameters.Add("memberId", memberId.ToString());

            var template = new UriTemplate("/2/events?member_id={memberId}&rsvp=yes,maybe,waitlist&access_token={token}");

            return await GetWithRetryAsync<EventsResponse>(_hostUri, template, parameters);
        }
    }
}
