namespace EventsLookup.Models.Navigation
{
    using System;

    /// <summary>
    /// Navigation Type class - Targets a specific View.
    /// </summary>
    public class NavType
    {
        /// <summary>
        /// Gets or sets Page type.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets Parameters sent to the Page.
        /// </summary>
        public string Parameter { get; set; }
    }
}
