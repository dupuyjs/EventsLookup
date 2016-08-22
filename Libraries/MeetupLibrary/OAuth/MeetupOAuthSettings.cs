// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace MeetupLibrary.OAuth
{
    /// <summary>
    /// A class that represents OAuth Settings
    /// </summary>
    public class MeetupOAuthSettings
    {
        /// <summary>
        /// Gets or sets Consumer Key
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets Consumer Secret
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Gets or sets Windows Store Id
        /// </summary>
        public string WindowsStoreId { get; set; }
    }
}
