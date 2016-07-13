using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsLookup.Models
{
    public class Favorite
    {
        public string DisplayName { get; set; }
        public int CategoryId { get; set; }
        public int TopicId { get; set; }

        public Favorite(string displayName, int categoryId, int topicId)
        {
            this.DisplayName = displayName;
            this.CategoryId = categoryId;
            this.TopicId = topicId;
        }

        public static List<Favorite> GetDefaultFavorites()
        {
            List<Favorite> favorites = new List<Favorite>();
            favorites.Add(new Favorite("BIG DATA", 34, 18062));
            favorites.Add(new Favorite("BOTS", 34, 1508598));
            favorites.Add(new Favorite("CLOUD COMPUTING", 34, 15167));
            favorites.Add(new Favorite("DEVOPS", 34, 87614));
            favorites.Add(new Favorite("IOT", 34, 79740));
            favorites.Add(new Favorite("MICROSERVICES", 34, 1504699));
            favorites.Add(new Favorite("MOBILE DEVELOPMENT", 34, 21441));
            favorites.Add(new Favorite("UI DESIGN", 34, 107583));
            favorites.Add(new Favorite("WEB DEVELOPMENT", 34, 15582));

            return favorites;
        }
    }
}
