// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace EventsLookup.Helpers
{
    using Windows.ApplicationModel.Resources;

    /// <summary>
    /// This class provides static helper methods for resources.
    /// </summary>
    public static class ResourceHelper
    {
        private static ResourceLoader _resources = null;
        private static ResourceLoader _errors = null;

        private static ResourceLoader Resources
        {
            get
            {
                return _resources ?? (_resources = ResourceLoader.GetForCurrentView("Resources"));
            }
        }

        private static ResourceLoader Errors
        {
            get
            {
                return _resources ?? (_resources = ResourceLoader.GetForCurrentView("Errors"));
            }
        }

        /// <summary>
        /// Get resource string
        /// </summary>
        /// <param name="path">Resource name.</param>
        /// <returns>Resource value.</returns>
        public static string GetResourceString(this string path)
        {
            return Resources.GetString(path);
        }

        /// <summary>
        /// Get error string
        /// </summary>
        /// <param name="path">Resource name.</param>
        /// <returns>Resource value.</returns>
        public static string GetErrorString(this string path)
        {
            return Errors.GetString(path);
        }
    }
}
