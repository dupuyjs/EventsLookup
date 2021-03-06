﻿// ******************************************************************
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
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents Categories response.
    /// </summary>
    public class CategoriesResponse
    {
        /// <summary>
        /// Gets list of the Category items.
        /// </summary>
        [JsonProperty("results")]
        public List<Category> Results { get; internal set; }

        /// <summary>
        /// Gets additional information about the response.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; internal set; }
    }
}
