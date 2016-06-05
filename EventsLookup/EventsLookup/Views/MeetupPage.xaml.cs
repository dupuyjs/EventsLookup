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
    }
}
