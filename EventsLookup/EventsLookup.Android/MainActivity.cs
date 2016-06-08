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
using HockeyApp;

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

            CrashManager.Register(this, "b4dba0f681c948999aa2e825e5690d11");

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            await Vm.InitAsync();

            ListViewMeetup.Adapter = Vm.Groups.GetAdapter(GetMeetupView);

            //MyButton.SetCommand(
            //    "Click",
            //    Vm.IncrementCommand);

            FeedbackManager.Register(this, "b4dba0f681c948999aa2e825e5690d11");


            MyButton.Click += delegate {
                FeedbackManager.ShowFeedbackActivity(ApplicationContext);
            };
            CheckForUpdates();
        }

        void CheckForUpdates()
        {
            // Remove this for store builds!
            UpdateManager.Register(this, "b4dba0f681c948999aa2e825e5690d11");
        }

        void UnregisterManagers()
        {
            UpdateManager.Unregister();
        }

        protected override void OnPause()
        {
            base.OnPause();

            UnregisterManagers();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnregisterManagers();
        }

        private View GetMeetupView(int position, Group group, View convertView)
        {
            View view = convertView ?? LayoutInflater.Inflate(Resource.Layout.RowGroup, null);

            var name = view.FindViewById<TextView>(Resource.Id.Name);
            var city = view.FindViewById<TextView>(Resource.Id.City);
            var members = view.FindViewById<TextView>(Resource.Id.Members);
            var organizer = view.FindViewById<TextView>(Resource.Id.Organizer);

            name.Text = group.Name;
            city.Text = group.City;
            members.Text = group.Members.ToString();
            organizer.Text = group.Organizer.Name;

            return view;
        }

    }
}

