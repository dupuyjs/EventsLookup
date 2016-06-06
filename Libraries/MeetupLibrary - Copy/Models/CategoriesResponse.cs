using Newtonsoft.Json;
using System.Collections.Generic;

namespace MeetupLibrary.Models
{
    public class CategoriesResponse
    {
        [JsonProperty("results")]
        public List<Category> Results { get; set; }
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
