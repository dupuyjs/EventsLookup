namespace EventsLookup.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Binding converter to convert a collection into a Boolean value.
    /// </summary>
    public class CollectionToBoolConverter : IValueConverter
    {
        /// <summary>
        /// Converts a collection into a Boolean value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>A <see cref="bool"/> value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isEnabled = false;
            IEnumerable<object> collection = value as IEnumerable<object>;

            if (collection != null && collection.Any())
            {
                isEnabled = true;
            }

            return isEnabled;
        }

        /// <summary>
        /// Not implemented.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>Throw <see cref="NotImplementedException"/> exception.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
