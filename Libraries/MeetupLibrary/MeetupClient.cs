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

        public async Task<CitiesResponse> GetCities(double latitude, double longitude, int radius = 50)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("latitude", latitude.ToString());
            parameters.Add("longitude", longitude.ToString());
            parameters.Add("radius", radius.ToString());

            var template = new UriTemplate("/2/cities?lat={latitude}&lon={longitude}&radius={radius}");

            return await GetWithRetryAsync<CitiesResponse>(_hostUri, template, parameters);
        }

        public async Task<EventsResponse> GetEvents(string query, string zip, int? category, string country = "FR")
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

        public async Task<List<Group>> GetGroups(int topicId, string zip, int? category, string country = "FR")
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);

            parameters.Add("topicid", topicId.ToString());
            if (!string.IsNullOrEmpty(zip)) parameters.Add("zip", zip);
            if (!string.IsNullOrEmpty(country)) parameters.Add("country", country);
            if (category.HasValue) parameters.Add("category", category.ToString());

            var template = new UriTemplate("/find/groups?key={apikey}&topic_id={topicid}&zip={zip}&category={category}&country={country}&sign=true");

            return await GetWithRetryAsync<List<Group>>(_hostUri, template, parameters);
        }

        public async Task<List<Topic>> GetTopics(string query, string lang = "en-US")
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("apikey", _apikey);

            if (!string.IsNullOrEmpty(query)) parameters.Add("query", Uri.EscapeDataString(query));
            if (!string.IsNullOrEmpty(lang)) parameters.Add("lang", lang);

            var template = new UriTemplate("/recommended/group_topics?key={apikey}&text={query}&lang={lang}&sign=true");

            return await GetWithRetryAsync<List<Topic>> (_hostUri, template, parameters);
        }
    }
}
