using EventsLookup.ViewModels;
using MeetupLibrary.Models;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EventsLookup.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MeetupPage : Page
    {
        private bool IsSearchOpened = false;
        private bool IsCalendarView = false;

        public MeetupViewModel Default
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MeetupViewModel>();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Task.Run(() => Default.Initialize());
        }

        public MeetupPage()
        {
            this.InitializeComponent();
        }

        private void OnItemClicked(object sender, TappedRoutedEventArgs e)
        {
            object value = null;
            var item = groupsList.SelectedItem as Group;

            if (item.AllEvents == null) return;

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

        private void OnAddTopic(object sender, RoutedEventArgs e)
        {
            if (!IsSearchOpened)
            {
                OpenSearch.Begin();
                IsSearchOpened = true;
            }
            else
            {
                CloseSearch.Begin();
                IsSearchOpened = false;
            }
        }

        private async void OnAddCalendar(object sender, TappedRoutedEventArgs e)
        {
            ListView listView = FindParent<ListView>(sender as Image);
            if (listView != null)
            {
                var item = (Event)listView.SelectedItem;

                var appointment = new Windows.ApplicationModel.Appointments.Appointment();
                appointment.Subject = item.Name;
                appointment.Location = item.Venue?.Name ?? string.Empty;
                appointment.StartTime = new DateTimeOffset(item.TimeWithOffset) ;
                //if (item.Duration.HasValue) appointment.Duration = new TimeSpan(item.Duration.Value * 1000);
                appointment.DetailsKind = Windows.ApplicationModel.Appointments.AppointmentDetailsKind.Html;
                appointment.Details = item.Description;

                // Get the selection rect of the button pressed to add this appointment
                var rect = GetElementRect(sender as FrameworkElement);

                // ShowAddAppointmentAsync returns an appointment id if the appointment given was added to the user's calendar.
                // This value should be stored in app data and roamed so that the appointment can be replaced or removed in the future.
                // An empty string return value indicates that the user canceled the operation before the appointment was added.
                String appointmentId = await Windows.ApplicationModel.Appointments.AppointmentManager.ShowAddAppointmentAsync(
                                       appointment, rect, Windows.UI.Popups.Placement.Default);
            }
        }

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
            if (IsCalendarView)
            {
                IsCalendarView = false;

                calendarList.Visibility = Visibility.Collapsed;
                groupsList.Visibility = Visibility.Visible;
                groupSectionTitle.Text = "GROUPS";

                switchButton.Icon = new SymbolIcon(Symbol.Calendar);
            }
            else
            {
                IsCalendarView = true;

                calendarList.Visibility = Visibility.Visible;
                groupsList.Visibility = Visibility.Collapsed;
                groupSectionTitle.Text = "CALENDAR";

                switchButton.Icon = new SymbolIcon(Symbol.AllApps);
            }
        }

        private async void OnGoToMeetup(object sender, TappedRoutedEventArgs e)
        {
            ListView listView = FindParent<ListView>(sender as Image);
            if (listView != null)
            {
                var item = (Event)listView.SelectedItem;

                var location = new Uri($"ms-drive-to:?destination.latitude={item.Venue.Latitude}&destination.longitude={item.Venue.Longitude}&destination.name={item.Venue.Name}");

                // Launch the Windows Maps app
                var launcherOptions = new Windows.System.LauncherOptions();
                launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
                var success = await Windows.System.Launcher.LaunchUriAsync(location, launcherOptions);
            }
        }
    }
}
