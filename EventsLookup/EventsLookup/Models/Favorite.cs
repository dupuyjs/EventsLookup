namespace EventsLookup.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /// <summary>
    /// A class that represents Favorite.
    /// </summary>
    public class Favorite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Favorite"/> class.
        /// </summary>
        /// <param name="displayName">A display name.</param>
        /// <param name="categoryId">A category ID.</param>
        /// <param name="topicId">A topic ID.</param>
        public Favorite(string displayName, int categoryId, int topicId)
        {
            this.DisplayName = displayName;
            this.CategoryId = categoryId;
            this.TopicId = topicId;
        }

        /// <summary>
        /// Gets Favorite's display name.
        /// </summary>
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets Favorite's category identifier.
        /// </summary>
        public int CategoryId { get; internal set; }

        /// <summary>
        /// Gets Favorite's topic identifier.
        /// </summary>
        public int TopicId { get; internal set; }

        /// <summary>
        /// Default Favorites
        /// </summary>
        /// <returns>A collection of Favorite object.</returns>
        public static ObservableCollection<Favorite> GetDefaultFavorites()
        {
            ObservableCollection<Favorite> favorites = new ObservableCollection<Favorite>();
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

        /// <summary>
        /// IComparer instance used to sort facorites alphabetically.
        /// </summary>
        /// <returns>A IComparer object.</returns>
        public static IComparer<Favorite> SortDisplayNameDescending()
        {
            return new SortFavoriteComparer();
        }

        private class SortFavoriteComparer : IComparer<Favorite>
        {
            public int Compare(Favorite x, Favorite y)
            {
                return string.Compare(x.DisplayName, y.DisplayName);
            }
        }
    }
}
