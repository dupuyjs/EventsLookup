// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace EventsLookup.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Binding converter to convert a collection into a Visibility enumeration.
    /// </summary>
    public class CollectionToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Converts a collection into a Visibility enumeration.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>A <see cref="Visibility"/> value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility visibility = Visibility.Visible;
            IEnumerable<object> collection = value as IEnumerable<object>;

            if (collection != null && collection.Any())
            {
                visibility = Visibility.Collapsed;
            }

            return visibility;
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
