using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using EventsLookup.Android.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using MeetupLibrary.Models;

namespace EventsLookup.Android
{
    [Activity(Label = "EventsLookup.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private MeetupViewModel _vm;
        private Button _myButton;
        private ListView _listViewMeetup;

        public MeetupViewModel Vm
        {
            get
            {
                return _vm ?? (_vm = new MeetupViewModel());
            }
        }

        public Button MyButton
        {
            get
            {
                return _myButton
                    ?? (_myButton = FindViewById<Button>(Resource.Id.MyButton));
            }
        }

        public ListView ListViewMeetup
        {
            get
            {
                return _listViewMeetup
                    ?? (_listViewMeetup = FindViewById<ListView>(Resource.Id.ListViewMeetup));
            }
        }

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            await Vm.InitAsync();

            ListViewMeetup.Adapter = Vm.Groups.GetAdapter(GetMeetupView);

            MyButton.SetCommand(
                "Click",
                Vm.IncrementCommand);
        }


        private View GetMeetupView(int position, Group group, View convertView)
        {
            View view = convertView ?? LayoutInflater.Inflate(Resource.Layout.RowGroup, null);

            var firstName = view.FindViewById<TextView>(Resource.Id.FirstName);
            var lastName = view.FindViewById<TextView>(Resource.Id.LastName);

            firstName.Text = group.Name;
            lastName.Text = group.City;

            return view;
        }

    }
}

