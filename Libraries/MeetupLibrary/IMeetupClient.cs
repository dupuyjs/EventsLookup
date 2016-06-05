using MeetupLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetupLibrary
{
    public interface IMeetupClient
    {
        Task<CitiesResponse> GetCities(string country = "fr");
        Task<EventsResponse> GetEvents(string query, string zip, int? category, string country = "fr");
        Task<EventsResponse> GetEvents(int groupId);
        Task<CategoriesResponse> GetCategories();
        Task<List<Group>> GetGroups(int topicId, string zip, int? category, bool upcomingOnly, string ordering = "most_active", string country = "fr");
        Task<List<Topic>> GetTopics(string query, string lang = "en-US");
    }
}
