using EventsLookup.ViewModels;
using MeetupLibrary.Authentication;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;
using Windows.Security.Authentication.Web;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EventsLookup.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public MeetupViewModel Default
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MeetupViewModel>();
            }
        }

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["MeetupApiKey"] = passwordBox.Password;            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //MeetupOAuthTokens tokens = new MeetupOAuthTokens();
            //tokens.ConsumerKey = @"t3k2f07e02hchau08g686o9dgi";
            //tokens.WindowsStoreId = @"ms-app://s-1-15-2-543302508-3874897725-3718165705-2024561646-3244720397-90261977-902163776/";
            ////tokens.WindowsStoreId = @"https://www.microsoft.com/fr-fr/";

            ////Uri uri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();

            //MeetupOAuthService service = new MeetupOAuthService();
            ////Test.NavigationCompleted += Test_NavigationCompleted;
            ////Test.Navigate(service.GetLoginUrl(tokens));

            //WebAuthenticationResult result = await WebAuthenticationBroker.AuthenticateAsync(
            //            WebAuthenticationOptions.None,
            //            service.GetLoginUrl(tokens),
            //            new System.Uri(tokens.WindowsStoreId));

            //if (result.ResponseStatus == WebAuthenticationStatus.Success)
            //{
            //    Debug.WriteLine(result.ResponseData.ToString());
            //    //OutputToken(WebAuthenticationResult.ResponseData.ToString());
            //    //await GetTwitterUserNameAsync(WebAuthenticationResult.ResponseData.ToString());
            //}

        }

        private void Test_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            //Debug.WriteLine(args.Uri);
        }
    }
}
