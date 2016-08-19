using MeetupLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupLibrary.Authentication
{
    public class MeetupOAuthService
    {
        private Uri _hostUri = new Uri("https://secure.meetup.com");

        public Uri GetLoginUrl(MeetupOAuthTokens tokens)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("key", tokens.ConsumerKey);
            parameters.Add("storeid", tokens.WindowsStoreId);

            var template = new UriTemplate("/oauth2/authorize?client_id={key}&response_type=token&set_mobile=on&redirect_uri={storeid}");

            return template.BindByName(_hostUri, parameters);
        }
    }
}
