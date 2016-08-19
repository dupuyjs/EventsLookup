namespace EventsLookup.Views
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using EventsLookup.ViewModels;
    using MeetupLibrary.Models;
    using Microsoft.Practices.ServiceLocation;
    using Windows.Foundation;
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
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!Default.IsDataLoaded)
            {
                Task.Run(() => Default.Initialize());
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

        //private async void OnAddCalendar(object sender, TappedRoutedEventArgs e)
        //{
        //    ListView listView = FindParent<ListView>(sender as Image);
        //    if (listView != null)
        //    {
        //        var item = (Event)listView.SelectedItem;

        //        var appointment = new Windows.ApplicationModel.Appointments.Appointment();
        //        appointment.Subject = item.Name;
        //        appointment.Location = item.Venue?.Name ?? string.Empty;
        //        appointment.StartTime = new DateTimeOffset(item.TimeWithOffset) ;
        //        //if (item.Duration.HasValue) appointment.Duration = new TimeSpan(item.Duration.Value * 1000);
        //        appointment.DetailsKind = Windows.ApplicationModel.Appointments.AppointmentDetailsKind.Html;
        //        appointment.Details = item.Description;

        //        // Get the selection rect of the button pressed to add this appointment
        //        var rect = GetElementRect(sender as FrameworkElement);

        //        // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user's calendar.
        //        // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
        //        // An empty string return value indicates that the user canceled the operation before the appointment was added.
        //        String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(
        //                               appointment, rect, Windows.UI.Popups.Placement.Default);
        //    }
        //}

        public static Rect GetElementRect(FrameworkElement element)
        {
            GeneralTransform buttonTransform = element.TransformToVisual(null);
            Point point = buttonTransform.TransformPoint(new Point());
            return new Rect(point, new Size(element.ActualWidth, element.ActualHeight));
        }

        public static T FindParent<T>(UIElement child) where T : DependencyObject
        {
            //get parent item
            UIElement parentObject = (UIElement)VisualTreeHelper.GetParent(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            T parent = parentObject as T;
            if (parent != null)
                return parent;
            else
                return FindParent<T>(parentObject);
        }

        private void SwitchView(object sender, RoutedEventArgs e)
        {
            if (_isCalendarView)
            {
                _isCalendarView = false;

                calendarList.Visibility = Visibility.Collapsed;
                groupsList.Visibility = Visibility.Visible;
                groupSectionTitle.Text = "GROUPS";

                switchButton.Icon = new SymbolIcon(Symbol.Calendar);
            }
            else
            {
                _isCalendarView = true;

                calendarList.Visibility = Visibility.Visible;
                groupsList.Visibility = Visibility.Collapsed;
                groupSectionTitle.Text = "CALENDAR";

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
    }
}
