using EventsLookup.ViewModels;
using MeetupLibrary.Models;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
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

        public MeetupViewModel Default
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MeetupViewModel>();
            }
        }

        public MeetupPage()
        {
            this.InitializeComponent();
        }


        private async void OnExport(object sender, TappedRoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string line = $"Group Name;Group Url;City;Members;Organizer;Next Event";
            sb.AppendLine(line);

            foreach (var item in Default.Groups)
            {
                line = $"{item?.Name};{item?.Link};{item?.City};{item?.Members};{item?.Organizer?.Name};{item?.NextEvent?.Name}";
                sb.AppendLine(line);
            }

            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;

            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("CSV file", new List<string>() { ".csv" });
            savePicker.SuggestedFileName = "meetup";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, sb.ToString());
                await CachedFileManager.CompleteUpdatesAsync(file);
            }
        }

        private void OnItemClicked(object sender, TappedRoutedEventArgs e)
        {
            object value = null;
            var item = groupsList.SelectedItem as Group;

            Grid template = sender as Grid;

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
            // Create an Appointment that should be added the user's appointments provider app.

            ListView listView = FindParent<ListView>(sender as Image);
            if (listView != null)
            {
                var item = (Event)listView.SelectedItem;

                var appointment = new Windows.ApplicationModel.Appointments.Appointment();
                appointment.Subject = item.Name;
                appointment.Location = item.Venue?.Name;
                appointment.StartTime = new DateTimeOffset(item.Time);
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

    }
}
