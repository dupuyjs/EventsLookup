namespace EventsLookup.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    /// Helper to sort alphabetically a collection.
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// Extension item. Add a specific item to a collection and sort it.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="list">The collection source.</param>
        /// <param name="item">The item to add.</param>
        /// <param name="comparer">The comparer object used to sort the list.</param>
        public static void AddSorted<T>(this IList<T> list, T item, IComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                comparer = Comparer<T>.Default;
            }

            int i = 0;
            while (i < list.Count && comparer.Compare(list[i], item) < 0)
            {
                i++;
            }

            list.Insert(i, item);
        }
    }
}
