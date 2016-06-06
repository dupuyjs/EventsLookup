using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupLibrary.Models
{
    public class Ordering
    {
        public static Dictionary<string, string> GetItems()
        {
            var items = new Dictionary<string, string>();

            items.Add("Distance", "distance");
            items.Add("Newest", "newest");
            items.Add("Most Active", "most_active");
            items.Add("Members", "members");

            return items;
        }
    }
}
