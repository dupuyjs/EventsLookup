using EventsLookup.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace EventsLookup.Converters
{
    public class FavoriteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (Favorite)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return null;
            return value;
        }
    }
}
