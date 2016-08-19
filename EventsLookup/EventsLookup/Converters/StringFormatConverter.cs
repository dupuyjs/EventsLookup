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
    using System.Globalization;
    using Windows.UI.Xaml.Data;

    /// <summary>
    /// Binding converter to display a formatted string.
    /// </summary>
    public class StringFormatConverter : IValueConverter
    {
        /// <summary>
        /// Return the formatted string version of the source object.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="language">The culture to use in the converter.</param>
        /// <returns>Formatted string.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (parameter != null)
            {
                string formatString = parameter as string;

                if (!string.IsNullOrEmpty(formatString))
                {
                    return string.Format(formatString, value);
                }
            }

            return value.ToString();
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
