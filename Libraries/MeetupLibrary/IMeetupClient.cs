using MeetupLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeetupLibrary
{
    public interface IMeetupClient
    {
        Task<CitiesResponse> GetCities(double lat, double lon, int radius);
        Task<EventsResponse> GetEvents(string query, string zip, int? category, string country = "FR");
        Task<CategoriesResponse> GetCategories();
        Task<List<Group>> GetGroups(int topicId, string zip, int? category, string country = "FR");
        Task<List<Topic>> GetTopics(string query, string lang = "en-US");
    }
}
