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
        private const string HOCKEY_APP_ID = "b4dba0f681c948999aa2e825e5690d11";

        private MeetupViewModel _vm;
        public MeetupViewModel Vm
        {
            get
            {
                return _vm ?? (_vm = new MeetupViewModel());
            }
        }

        private Button _feedbackButton;
        public Button FeedbackButton
        {
            get
            {
                return _feedbackButton
                    ?? (_feedbackButton = FindViewById<Button>(Resource.Id.FeedbackButton));
            }
        }

        private Button _crashButton;
        public Button CrashButton
        {
            get
            {
                return _crashButton
                    ?? (_crashButton = FindViewById<Button>(Resource.Id.CrashButton));
            }
        }

        private Button _helloButton;
        public Button HelloButton
        {
            get
            {
                return _helloButton
                    ?? (_helloButton = FindViewById<Button>(Resource.Id.HelloButton));
            }
        }

        private ListView _listViewMeetup;
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

            #region HockeyApp Distribution

            CheckForUpdates();

            #endregion

            #region Feedback and Monitoring

            //HockeyApp.Metrics.MetricsManager.Register(this, Application, HOCKEY_APP_ID);
            //HockeyApp.Metrics.MetricsManager.TrackEvent("Started");

            CrashManager.Register(this, HOCKEY_APP_ID);
            FeedbackManager.Register(this, HOCKEY_APP_ID);

            #endregion

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            await Vm.InitAsync();

            // Binding Solution 

            this.SetBinding(
                () => Vm.Hello,
                () => HelloButton.Text);

            HelloButton.SetCommand(
                "Click",
                Vm.IncrementCommand);

            CrashButton.SetCommand(
                "Click",
                Vm.CrashCommand);

            ListViewMeetup.Adapter = Vm.Groups.GetAdapter(GetMeetupView);

            // Traditional Handler
            FeedbackButton.Click += delegate {
                FeedbackManager.ShowFeedbackActivity(ApplicationContext);
            };
        }

        #region HockeyApp Distribution

        void CheckForUpdates()
        {
            UpdateManager.Register(this, HOCKEY_APP_ID);
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

        #endregion

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

