// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

using System.Collections.Generic;

namespace MeetupLibrary.Models
{
    /// <summary>
    /// This class provides static converter method for Ordering enumeration.
    /// </summary>
    public static class Ordering
    {
        /// <summary>
        /// Converts an Ordering enumeration to a user friendly string.
        /// </summary>
        /// <param name="ordering"><see cref="OrderingEnum"/> enumeration.</param>
        /// <returns>User friendly string.</returns>
        public static string ToFriendlyString(this OrderingEnum ordering)
        {
            var distance = "distance";
            var newest = "newest";
            var members = "members";
            var mostActive = "most_active";

            switch (ordering)
            {
                case OrderingEnum.Distance:
                    return distance;
                case OrderingEnum.Newest:
                    return newest;
                case OrderingEnum.Members:
                    return members;
                case OrderingEnum.MostActive:
                    return mostActive;
                default:
                    return mostActive;
            }
        }

        /// <summary>
        /// Converts a string to an Ordering enumeration value.
        /// </summary>
        /// <param name="ordering">User friendly string.</param>
        /// <returns><see cref="OrderingEnum"/> enumeration value.</returns>
        public static OrderingEnum? ToOrdering(this string ordering)
        {
            if (string.Equals(ordering, OrderingEnum.Distance.ToFriendlyString()))
            {
                return OrderingEnum.Distance;
            }

            if (string.Equals(ordering, OrderingEnum.Newest.ToFriendlyString()))
            {
                return OrderingEnum.Newest;
            }

            if (string.Equals(ordering, OrderingEnum.Members.ToFriendlyString()))
            {
                return OrderingEnum.Members;
            }

            if (string.Equals(ordering, OrderingEnum.MostActive.ToFriendlyString()))
            {
                return OrderingEnum.MostActive;
            }

            return null;
        }

        /// <summary>
        /// Gets OrderingEnums items.
        /// </summary>
        /// <returns>A dictionary with OrderingEnum items and friendly names associated.</returns>
        public static Dictionary<string, OrderingEnum> GetItems()
        {
            var items = new Dictionary<string, OrderingEnum>();

            items.Add("Distance", OrderingEnum.Distance);
            items.Add("Newest", OrderingEnum.Newest);
            items.Add("Most Active", OrderingEnum.MostActive);
            items.Add("Members", OrderingEnum.Members);

            return items;
        }
    }
}
