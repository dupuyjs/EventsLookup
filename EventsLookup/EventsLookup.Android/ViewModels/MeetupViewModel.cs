using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MeetupLibrary.Models;
using GalaSoft.MvvmLight.Threading;
using MeetupLibrary;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using HockeyApp;

namespace EventsLookup.Android.ViewModels
{
    public class MeetupViewModel : ViewModelBase
    {
        public ObservableCollection<Group> Groups { get; private set; }

        private IMeetupClient _meetupProxy = null;
        public IMeetupClient MeetupProxy
        {
            get
            {
                return this._meetupProxy ?? (_meetupProxy = MeetupClientFactory.CreateMeetupClient("5913182f503a73342030315a255b5b40"));
            }
        }

        #region Initialisation

        public async Task InitAsync()
        {
            if (Groups != null)
            {
                var groupsCopy = Groups.ToList();
                Groups = new ObservableCollection<Group>(groupsCopy);
                return;
            }

            Groups = new ObservableCollection<Group>();

            var groups = await MeetupProxy.GetGroups(21441, "meetup1", 34, false, "most_active");
            Groups.Clear();
            foreach (var group in groups)
            {
                Groups.Add(group);
            }
        }

        public async Task<bool> GetGroups(int topicId, string zip, int categoryId, bool upcomingOnly, string ordering)
        {
            try
            {
                var groups = await MeetupProxy.GetGroups(topicId, zip, categoryId, upcomingOnly, ordering);
                this.Groups = new ObservableCollection<Group>(groups);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return true;
        }

        #endregion

        #region Binding

        private int _index;

        private string _hello = "Hello!";
        public string Hello
        {
            get
            {
                return _hello;
            }
            set
            {
                Set(() => Hello, ref _hello, value);
            }
        }

        private RelayCommand _incrementCommand;
        public RelayCommand IncrementCommand
        {
            get
            {
                return _incrementCommand
                    ?? (_incrementCommand = new RelayCommand(
                    () =>
                    {
                        Hello = string.Format("{0} click(s)", ++_index);
                    }));
            }
        }

        private RelayCommand _crashCommand;
        public RelayCommand CrashCommand
        {
            get
            {
                return _crashCommand
                    ?? (_crashCommand = new RelayCommand(
                    () =>
                    {
                        throw new NotImplementedException();
                    }));
            }
        }

        #endregion
    }
}