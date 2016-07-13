using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetupLibrary.Models;
using MeetupLibrary.Helpers;

namespace MeetupLibrary
{
    public sealed class MeetupClient : SimpleServiceClient, IMeetupClient
    {
        private Uri _hostUri = new Uri("https://api.meetup.com");
        private string _apikey;

        internal MeetupClient(string apiKey)
        {
            this._apikey = apiKey;
        }

        public async Task<CitiesResponse> GetCities(string country = "fr")
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("country", country.ToString());

            var template = new UriTemplate("/2/cities?country={country}&order=size&page=50");

            return await GetWithRetryAsync<CitiesResponse>(_hostUri, template, parameters);
        }

        public async Task<EventsResponse> GetEvents(string query, string zip, int? category, string country = "fr")
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);

            if (!string.IsNullOrEmpty(query)) parameters.Add("query", Uri.EscapeDataString(query));
            if (!string.IsNullOrEmpty(zip)) parameters.Add("zip", zip);
            if (!string.IsNullOrEmpty(country)) parameters.Add("country", country);
            if (category.HasValue) parameters.Add("category", category.ToString());

            var template = new UriTemplate("/2/open_events?key={apikey}&text={query}&category={category}&zip={zip}&country={country}&sign=true");

            return await GetWithRetryAsync<EventsResponse>(_hostUri, template, parameters);
        }

        public async Task<CategoriesResponse> GetCategories()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);

            var template = new UriTemplate("/2/categories?key={apikey}&sign=true");

            return await GetWithRetryAsync<CategoriesResponse>(_hostUri, template, parameters);
        }

        public async Task<List<Group>> GetGroups(int topicId, string zip, int? category, bool upcomingOnly, string ordering = "most_active", string country = "fr")
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);

            parameters.Add("topicid", topicId.ToString());
            if (!string.IsNullOrEmpty(zip)) parameters.Add("zip", zip);
            if (!string.IsNullOrEmpty(country)) parameters.Add("country", country);
            if (!string.IsNullOrEmpty(ordering)) parameters.Add("ordering", ordering);
            if (category.HasValue) parameters.Add("category", category.ToString());
            parameters.Add("upcomingonly", upcomingOnly.ToString());

            var template = new UriTemplate("/find/groups?key={apikey}&radius=50&topic_id={topicid}&zip={zip}&category={category}&order={ordering}&upcoming_events={upcomingonly}&country={country}&page=200&sign=true");

            return await GetWithRetryAsync<List<Group>>(_hostUri, template, parameters);
        }

        public async Task<EventsResponse> GetEvents(int groupId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);
            parameters.Add("groupId", groupId.ToString());

            var template = new UriTemplate("/2/events?key={apikey}&group_id={groupId}&sign=true");

            return await GetWithRetryAsync<EventsResponse>(_hostUri, template, parameters);
        }

        public async Task<List<Topic>> GetTopics(string query, string lang = "en-US")
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);

            if (!string.IsNullOrEmpty(query)) parameters.Add("query", Uri.EscapeDataString(query));
            if (!string.IsNullOrEmpty(lang)) parameters.Add("lang", lang);

            var template = new UriTemplate("/recommended/group_topics?key={apikey}&text={query}&lang={lang}&sign=true");

            return await GetWithRetryAsync<List<Topic>>(_hostUri, template, parameters);
        }

        public async Task<Member> GetUserProfile()
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);

            var template = new UriTemplate("/2/member/self?key={apikey}");

            return await GetWithRetryAsync<Member>(_hostUri, template, parameters);
        }

        public async Task<EventsResponse> GetUserCalendar(int memberId)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);
            parameters.Add("memberId", memberId.ToString());

            var template = new UriTemplate("/2/events?key={apikey}&member_id={memberId}&rsvp=yes,maybe,waitlist&sign=true");

            return await GetWithRetryAsync<EventsResponse>(_hostUri, template, parameters);
        }
    }
}
