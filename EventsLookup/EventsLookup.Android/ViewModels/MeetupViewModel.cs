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
        public async Task InitAsync()
        {
            if (Groups != null)
            {
                // Prevent memory leak in Android
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

        public ObservableCollection<Group> Groups { get; private set; }

        //}

        //private List<Group> _groups = null;
        //public List<Group> Groups
        //{
        //    get
        //    {
        //        return _groups;
        //    }
        //    set
        //    {
        //        _groups = value;
        //        DispatcherHelper.CheckBeginInvokeOnUI(() =>
        //        {
        //            RaisePropertyChanged(() => Groups);
        //        });
        //    }
        //}

        private IMeetupClient _meetupProxy = null;
        public IMeetupClient MeetupProxy
        {
            get
            {
                return this._meetupProxy ?? (_meetupProxy = MeetupClientFactory.CreateMeetupClient("5913182f503a73342030315a255b5b40"));
            }
        }

        public async Task<bool> GetGroups(int topicId, string zip, int categoryId, bool upcomingOnly, string ordering)
        {
            try
            {
                var groups = await MeetupProxy.GetGroups(topicId, zip, categoryId, upcomingOnly, ordering);
                this.Groups = new ObservableCollection<Group>(groups);
                //this.GroupsNumber = groups.Count();

                //foreach (var item in Groups)
                //{
                //    if (item.NextEvent != null)
                //    {
                //        var events = await MeetupProxy.GetEvents(item.Id);
                //        item.AllEvents = events.Results;
                //    }
                //}
            }
            catch (Exception)
            {
                //var loader = new ResourceLoader("Errors");
                //DialogService.DisplayError(loader.GetString("Network/Caption"), loader.GetString("Network/Message"), "");
            }

            return true;
        }

        private int _index;

        public const string HelloPropertyName = "Hello";

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
   
                        //await this.GetGroups(21441, "meetup1", 34, false, "most_active");
                        //Hello = string.Format("Hello! {0} click(s)", ++_index);
                    }));
            }
        }

    }
}