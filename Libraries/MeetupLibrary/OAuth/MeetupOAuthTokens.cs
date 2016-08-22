namespace MeetupLibrary.OAuth
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that represents OAuth Tokens
    /// </summary>
    public class MeetupOAuthTokens
    {
        /// <summary>
        /// Gets Access Token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Gets Refresh Token
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; internal set; }

        /// <summary>
        /// Gets Token Type
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; internal set; }

        /// <summary>
        /// Gets Token Expiration
        /// </summary>
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; internal set; }

        /// <summary>
        /// Gets TimeStamp
        /// </summary>
        public DateTime TimeStamp { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether OAuth access token is expired
        /// </summary>
        public bool IsTokenExpired
        {
            get
            {
                bool isExpired = true;

                TimeSpan interval = DateTime.Now - TimeStamp;
                if (interval.Seconds < 3600)
                {
                    isExpired = false;
                }

                return isExpired;
            }
        }
    }
}
