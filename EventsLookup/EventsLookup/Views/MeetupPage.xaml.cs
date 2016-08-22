// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace EventsLookup.Views
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EventsLookup.ViewModels;
    using Helpers;
    using MeetupLibrary.Models;
    using MeetupLibrary.OAuth;
    using Microsoft.Practices.ServiceLocation;
    using Windows.Foundation;
    using Windows.Security.Authentication.Web;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Input;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Animation;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Main Page - Display Meetup Details
    /// </summary>
    public sealed partial class MeetupPage : Page
    {
        private bool _isSearchOpened = false;
        private bool _isCalendarView = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetupPage"/> class.
        /// </summary>
        public MeetupPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets MeetupViewModel default instance.
        /// </summary>
        public MeetupViewModel Default
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MeetupViewModel>();
            }
        }

        /// <summary>
        /// Starts the authentication operation.
        /// </summary>
        /// <returns>OAuth tokens.</returns>
        public async Task<MeetupOAuthTokens> AuthenticateUser()
        {
            MeetupOAuthTokens tokens = null;

            MeetupOAuthSettings settings = new MeetupOAuthSettings();
            settings.ConsumerKey = Keys.ConsumerKey;
            settings.ConsumerSecret = Keys.ConsumerSecret;
            settings.WindowsStoreId = Keys.WindowsStoreId;

            MeetupOAuthService.Instance.Initialize(settings);

            WebAuthenticationResult authenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                        WebAuthenticationOptions.None,
                        MeetupOAuthService.Instance.GetLoginUrl(),
                        new System.Uri(settings.WindowsStoreId));

            if (authenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                var responseData = authenticationResult.ResponseData;
                var responseUri = new Uri(responseData);

                var decoder = new WwwFormUrlDecoder(responseUri.Query);
                var code = decoder.GetFirstValueByName("code");

                tokens = await MeetupOAuthService.Instance.GetOAuthTokens(code);

                Default.IsUserAuthenticated = true;
            }

            return tokens;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!Default.IsUserAuthenticated)
            {
                var tokens = await AuthenticateUser();
            }

            if (!Default.IsDataLoaded && Default.IsUserAuthenticated)
            {
                Task.Run(() => Default.Initialize());
            }
        }

        private static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(default(Point));
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        private static T FindParent<T>(UIElement child)
            where T : DependencyObject
        {
            UIElement parentObject = (UIElement)VisualTreeHelper.GetParent(child);

            if (parentObject == null)
            {
                return null;
            }

            T parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }

        private void OnItemClicked(object sender, TappedRoutedEventArgs e)
        {
            object value = null;
            var item = groupsList.SelectedItem as Group;

            if (item.AllEvents != null && item.AllEvents.Any())
            {
                UIElement child = sender as UIElement;
                var template = FindParent<Grid>(child);

                if (item.IsEventsVisible)
                {
                    item.IsEventsVisible = false;
                    template?.Resources.TryGetValue("ExpandCloseAnimation", out value);
                }
                else
                {
                    item.IsEventsVisible = true;
                    template?.Resources.TryGetValue("ExpandOpenAnimation", out value);
                }

                Storyboard storyboard = value as Storyboard;
                storyboard?.Begin();
            }
        }

        private void OnAddTopic(object sender, RoutedEventArgs e)
        {
            if (!_isSearchOpened)
            {
                OpenSearch.Begin();
                _isSearchOpened = true;
            }
            else
            {
                CloseSearch.Begin();
                _isSearchOpened = false;
            }
        }

        private async void OnAddCalendar(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var context = button.DataContext;

            if (context != null)
            {
                var item = context as Event;

                var appointment = new Windows.ApplicationModel.Appointments.Appointment();
                appointment.Subject = item.Name;
                appointment.Location = item.Venue?.Name ?? string.Empty;
                appointment.StartTime = new DateTimeOffset(item.TimeWithOffset);
                appointment.DetailsKind = Windows.ApplicationModel.Appointments.AppointmentDetailsKind.Html;
                appointment.Details = item.Description;

                // Get the selection rect of the button pressed to add this appointment
                var rect = GetElementRect(sender as FrameworkElement);

                string appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(
                                       appointment, rect, Windows.UI.Popups.Placement.Default);
            }
        }

        private void SwitchView(object sender, RoutedEventArgs e)
        {
            if (_isCalendarView)
            {
                _isCalendarView = false;

                calendarList.Visibility = Visibility.Collapsed;
                groupsList.Visibility = Visibility.Visible;
                groupSectionTitle.Text = ResourceHelper.GetResourceString("GroupsTitle/Text");

                switchButton.Icon = new SymbolIcon(Symbol.Calendar);
            }
            else
            {
                _isCalendarView = true;

                calendarList.Visibility = Visibility.Visible;
                groupsList.Visibility = Visibility.Collapsed;
                groupSectionTitle.Text = ResourceHelper.GetResourceString("CalendarTitle/Text");

                switchButton.Icon = new SymbolIcon(Symbol.AllApps);
            }
        }

        private async void OnGoToMeetup(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var context = button.DataContext;

            if (context != null)
            {
                var item = context as Event;
                var location = new Uri($"ms-drive-to:?destination.latitude={item.Venue.Latitude}&destination.longitude={item.Venue.Longitude}&destination.name={item.Venue.Name}");

                // Launch the Windows Maps app
                var launcherOptions = new Windows.System.LauncherOptions();
                launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
                var success = await Windows.System.Launcher.LaunchUriAsync(location, launcherOptions);
            }
        }

        private async void OnGetInformation(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var context = button.DataContext;

            if (context != null)
            {
                var item = context as Event;
                var eventUrl = new Uri(item.EventUrl);

                var success = await Windows.System.Launcher.LaunchUriAsync(eventUrl);
            }
        }
    }
}
