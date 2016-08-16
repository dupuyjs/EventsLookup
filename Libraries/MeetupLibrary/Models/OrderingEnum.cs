// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace MeetupLibrary.Models
{
    /// <summary>
    /// Ordering enumeration.
    /// </summary>
    public enum OrderingEnum
    {
        /// <summary>
        /// Order by distance.
        /// </summary>
        Distance,

        /// <summary>
        /// Order by date group was founded.
        /// </summary>
        Newest,

        /// <summary>
        /// Order by number of members.
        /// </summary>
        Members,

        /// <summary>
        /// Order by group with most active members.
        /// </summary>
        MostActive
    }
}
