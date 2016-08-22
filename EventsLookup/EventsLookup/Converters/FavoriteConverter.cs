namespace EventsLookup.Converters
{
    using System;
    using EventsLookup.Models;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Cast an object to a Favorite type.
    /// </summary>
    public class FavoriteConverter : IValueConverter
    {
        /// <summary>
        /// Cast an object to a Favorite type.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>A <see cref="Favorite"/> object.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (Favorite)value;
        }

        /// <summary>
        /// Return default value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>The source value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
