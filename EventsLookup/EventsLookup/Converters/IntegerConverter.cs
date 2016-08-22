namespace EventsLookup.Converters
{
    using System;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Cast an object to integer value.
    /// </summary>
    public class IntegerConverter : IValueConverter
    {
        /// <summary>
        /// Cast an object to an integer value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>An integer value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)value;
        }

        /// <summary>
        /// Ensure value returned is not null.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>The source value. Except if null, return 0 instead.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return 0;
            }

            return value;
        }
    }
}
