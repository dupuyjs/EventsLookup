namespace MeetupLibrary.OAuth
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net.Http;
    using System.Threading.Tasks;
    using MeetupLibrary.Helpers;
    using Newtonsoft.Json;

    /// <summary>
    /// A class that represents OAuth service to access Meetup API Platform.
    /// </summary>
    public class MeetupOAuthService : IDisposable
    {
        private static MeetupOAuthService _instance = null;

        private Uri _authoriseUri = new Uri("https://secure.meetup.com/oauth2/authorize");
        private Uri _accessUri = new Uri("https://secure.meetup.com/oauth2/access");

        private MeetupOAuthSettings _settings = null;
        private MeetupOAuthTokens _tokens = null;
        private HttpClient _httpClient = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetupOAuthService"/> class.
        /// </summary>
        public MeetupOAuthService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets Singleton Instance of the <see cref="MeetupOAuthService"/> class.
        /// </summary>
        public static MeetupOAuthService Instance => _instance ?? (_instance = new MeetupOAuthService());

        /// <summary>
        /// Initialization with OAuth settings.
        /// </summary>
        /// <param name="settings">OAuth settings.</param>
        public void Initialize(MeetupOAuthSettings settings)
        {
            _settings = settings;
        }

        /// <summary>
        /// Initialization with OAuth settings and MeetupOAuthTokens tokens.
        /// </summary>
        /// <param name="settings">OAuth settings.</param>
        /// <param name="tokens">MeetupOAuthTokens tokens.</param>
        public void Initialize(MeetupOAuthSettings settings, MeetupOAuthTokens tokens)
        {
            _settings = settings;
            _tokens = tokens;
        }

        /// <summary>
        /// Gets Url to request authorization.
        /// </summary>
        /// <returns>Authorization Url.</returns>
        public Uri GetLoginUrl()
        {
            if (_settings == null)
            {
                throw new Exception("MeetupOAuthSettings Not Initialized");
            }

            var parameters = new Dictionary<string, string>();
            parameters.Add("key", _settings.ConsumerKey);
            parameters.Add("storeid", _settings.WindowsStoreId);

            var template = new UriTemplate("?client_id={key}&response_type=code&set_mobile=on&redirect_uri={storeid}");

            return template.BindByName(_authoriseUri, parameters);
        }

        /// <summary>
        /// Gets OAuth Access Token
        /// </summary>
        /// <param name="code">A string that can only be used once to request an access token.</param>
        /// <returns>The OAuth tokens</returns>
        public async Task<MeetupOAuthTokens> GetOAuthTokens(string code)
        {
            if (_settings == null)
            {
                throw new Exception("MeetupOAuthSettings Not Initialized");
            }

            MeetupOAuthTokens tokens = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();

            if (_tokens == null)
            {
                if (string.IsNullOrWhiteSpace(code))
                {
                    throw new ArgumentException("Access Code Not Valid");
                }

                // Requesting Access Token
                parameters.Add("client_id", _settings.ConsumerKey);
                parameters.Add("client_secret", _settings.ConsumerSecret);
                parameters.Add("grant_type", "authorization_code");
                parameters.Add("redirect_uri", _settings.WindowsStoreId);
                parameters.Add("code", code);
            }
            else
            {
                if (!_tokens.IsTokenExpired)
                {
                    return _tokens;
                }

                // Refreshing Access Token
                parameters.Add("client_id", _settings.ConsumerKey);
                parameters.Add("client_secret", _settings.ConsumerSecret);
                parameters.Add("grant_type", "refresh_token");
                parameters.Add("refresh_token", _tokens.RefreshToken);
            }

            FormUrlEncodedContent requestContent = new FormUrlEncodedContent(parameters);

            try
            {
                var httpMessage = await _httpClient.PostAsync(_accessUri, requestContent);

                if (httpMessage.IsSuccessStatusCode)
                {
                    var responseContent = httpMessage.Content;
                    var jsonContent = await responseContent.ReadAsStringAsync();

                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.MissingMemberHandling = MissingMemberHandling.Ignore;

                    _tokens = JsonConvert.DeserializeObject<MeetupOAuthTokens>(jsonContent);
                    _tokens.TimeStamp = DateTime.Now;

                    tokens = _tokens;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

            return tokens;
        }

        /// <summary>
        /// Gets OAuth Access Token
        /// </summary>
        /// <returns>The OAuth Access Token</returns>
        public async Task<string> GetAccessToken()
        {
            var tokens = await GetOAuthTokens(string.Empty);
            return tokens.AccessToken;
        }

        /// <summary>
        /// Use this method to close or release unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Use this method to close or release unmanaged resources.
        /// </summary>
        /// <param name="disposing">Boolean value.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_httpClient != null)
                {
                    _httpClient.Dispose();
                    _httpClient = null;
                }
            }
        }
    }
}
