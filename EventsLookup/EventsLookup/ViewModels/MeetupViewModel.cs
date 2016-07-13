using EventsLookup.Helpers;
using EventsLookup.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using MeetupLibrary;
using MeetupLibrary.Models;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace EventsLookup.ViewModels
{
    public class MeetupViewModel : ViewModelBase
    {

        #region Meetup Proxy

        private IMeetupClient _meetupProxy = null;
        public IMeetupClient MeetupProxy
        {
            get
            {
                return this._meetupProxy ?? (_meetupProxy = MeetupClientFactory.CreateMeetupClient(Keys.MeetupApiKey));
            }
        }

        #endregion

        #region Properties

        private List<Group> _groups = null;
        public List<Group> Groups
        {
            get
            {
                return _groups;
            }
            set
            {
                _groups = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Groups);
                });
            }
        }

        private List<Event> _calendar = null;
        public List<Event> Calendar
        {
            get
            {
                return _calendar;
            }
            set
            {
                _calendar = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Calendar);
                });
            }
        }

        private IOrderedEnumerable<IGrouping<DateTime, Event>> _meetups = null;
        public IOrderedEnumerable<IGrouping<DateTime, Event>> Meetups
        {
            get
            {
                return _meetups;
            }
            set
            {
                _meetups = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Meetups);
                });
            }
        }

        private bool _isLoading = true;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => IsLoading);
                });
            }
        }

        private bool _isGroupsEmpty = false;
        public bool IsGroupsEmpty
        {
            get
            {
                return _isGroupsEmpty;
            }
            set
            {
                _isGroupsEmpty = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => IsGroupsEmpty);
                });
            }
        }

        private bool _isUserMeetupsEmpty = false;
        public bool IsUserMeetupsEmpty
        {
            get
            {
                return _isUserMeetupsEmpty;
            }
            set
            {
                _isUserMeetupsEmpty = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => IsUserMeetupsEmpty);
                });
            }
        }

        private Member _user = null;
        public Member User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => User);
                });
            }
        }


        private List<City> _cities = null;
        public List<City> Cities
        {
            get
            {
                return _cities;
            }
            set
            {
                _cities = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Cities);
                });
            }
        }

        string _selectedCity = string.Empty;
        public string SelectedCity
        {
            get
            {
                return _selectedCity;
            }
            set
            {
                if (_selectedCity != value)
                { 
                    _selectedCity = value;
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        RaisePropertyChanged(() => SelectedCity);
                    });
                }
            }
        }

        private List<Category> _categories = null;
        public List<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Categories);
                });
            }
        }

        int _selectedCategory = default(int);
        public int SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                if (_selectedCategory != value)
                {
                    _selectedCategory = value;
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        RaisePropertyChanged(() => SelectedCategory);
                    });
                }
            }
        }

        private Dictionary<string, string> _ordering = null;
        public Dictionary<string, string> Ordering
        {
            get
            {
                return _ordering;
            }
            set
            {
                _ordering = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Ordering);
                });
            }
        }

        string _selectedOrdering = string.Empty;
        public string SelectedOrdering
        {
            get
            {
                return _selectedOrdering;
            }
            set
            {
                if (_selectedOrdering != value)
                {
                    _selectedOrdering = value;
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        RaisePropertyChanged(() => SelectedOrdering);
                    });
                }
            }
        }

        private List<Topic> _topics = null;
        public List<Topic> Topics
        {
            get
            {
                return _topics;
            }
            set
            {
                _topics = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Topics);
                });
            }
        }

        int _selectedTopic = default(int);
        public int SelectedTopic
        {
            get
            {
                return _selectedTopic;
            }
            set
            {
                if (_selectedTopic != value)
                {
                    _selectedTopic = value;
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        RaisePropertyChanged(() => SelectedTopic);
                    });
                }
            }
        }

        private List<Favorite> _favorites = null;
        public List<Favorite> Favorites
        {
            get
            {
                return _favorites;
            }
            set
            {
                _favorites = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Favorites);
                });
            }
        }

        Favorite _selectedFavorite = null;
        public Favorite SelectedFavorite
        {
            get
            {
                return _selectedFavorite;
            }
            set
            {
                if (_selectedFavorite != value)
                {
                    _selectedFavorite = value;
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        RaisePropertyChanged(() => SelectedFavorite);
                    });
                }
            }
        }

        //private bool? _upcoming = false;
        //public bool? Upcoming
        //{
        //    get
        //    {
        //        return _upcoming;
        //    }
        //    set
        //    {
        //        _upcoming = value;
        //        DispatcherHelper.CheckBeginInvokeOnUI(() =>
        //        {
        //            RaisePropertyChanged(() => Upcoming);
        //        });
        //    }
        //}

        private int _groupsCount = 0;
        public int GroupsCount
        {
            get
            {
                return _groupsCount;
            }
            set
            {
                _groupsCount = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => GroupsCount);
                });
            }
        }

        private string _keywords= "android";
        public string Keywords
        {
            get
            {
                return _keywords;
            }
            set
            {
                _keywords = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Keywords);
                });
            }
        }

        public Services.DialogService.IDialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Services.DialogService.IDialogService>();
            }
        }

        #endregion

        #region Caching

        private Dictionary<int, List<Event>> _cacheManager = null;
        public Dictionary<int, List<Event>> CacheManager
        {
            get
            {
                return _cacheManager ?? (_cacheManager = new Dictionary<int, List<Event>>());
            }
            set
            {
                _cacheManager = value;
            }
        }

        #endregion

        public MeetupViewModel()
        {
            Task.Run(() => Initialize());
        }

        #region Initialization

        public async Task<bool> Initialize()
        {
            try
            {
                this.Favorites = Favorite.GetDefaultFavorites();

                Member member = await MeetupProxy.GetUserProfile();
                this.User = member;

                var calendar = await MeetupProxy.GetUserCalendar(member.Id);
                this.Calendar = calendar.Results;

                var cities = await MeetupProxy.GetCities();
                this.Cities = cities.Results;

                var categories = await MeetupProxy.GetCategories();
                this.Categories = categories.Results;

                var ordering = MeetupLibrary.Models.Ordering.GetItems();
                this.Ordering = ordering;

                this.SelectedCity = "meetup1";
                this.SelectedCategory = 34;
                this.SelectedOrdering = "most_active";

                this.IsUserMeetupsEmpty = (Calendar.Count == 0) ? true : false;

                this.SelectedFavorite = this.Favorites.First();
            }
            catch (Exception)
            {
                var loader = new ResourceLoader("Errors");
                DialogService.DisplayError(loader.GetString("Network/Caption"), loader.GetString("Network/Message"), "");
            }

            return true;
        }

        #endregion

        #region Get Topics

        public async void OnKeywordsQuery(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                var topics = await MeetupProxy.GetTopics(Keywords);
                this.Topics = topics;
                this.SelectedTopic = topics.First().Id;
            }
            catch (Exception)
            {
                var loader = new ResourceLoader("Errors");
                DialogService.DisplayError(loader.GetString("Network/Caption"), loader.GetString("Network/Message"), "");
            }
        }

        #endregion

        public async Task<bool> GetGroups(int topicId, string zip, int categoryId, bool upcomingOnly, string ordering)
        {
            try
            {
                List<Group> groups = null;
                List<Event> meetups = new List<Event>();

                groups = await MeetupProxy.GetGroups(topicId, zip, categoryId, upcomingOnly, ordering);

                this.Groups = groups;
                this.GroupsCount= groups.Count();

                this.IsGroupsEmpty = (GroupsCount == 0) ? true : false;

                foreach (var item in Groups)
                {
                    if (item.AllEvents == null && item.NextEvent != null)
                    {
                        List<Event> events = null;

                        if (CacheManager.ContainsKey(item.Id))
                        {
                            if (CacheManager[item.Id] != null) events = CacheManager[item.Id];
                        }
                        else
                        {
                            events = (await MeetupProxy.GetEvents(item.Id)).Results;
                            CacheManager.Add(item.Id, events);
                        }

                        item.AllEvents = events;
                        meetups.AddRange(events);
                    }
                } //foreach

                var query = from meetup in meetups
                            orderby meetup.Time
                            group meetup by new DateTime(meetup.Time.Year, meetup.Time.Month, 1) into g
                            orderby g.Key
                            select g;

                this.Meetups = query;
            }
            catch (Exception)
            {
                var loader = new ResourceLoader("Errors");
                DialogService.DisplayError(loader.GetString("Network/Caption"), loader.GetString("Network/Message"), "");
            }

            return true;
        }

        public async void OnFavoriteChanged(object sender, SelectionChangedEventArgs e)
        {
            this.IsLoading = true;

            var favorite = this.SelectedFavorite;
            var zip = this.SelectedCity;
            var ordering = this.SelectedOrdering;

            await GetGroups(favorite.TopicId, zip, favorite.CategoryId, false, ordering);

            this.IsLoading = false;
        }

        public async void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoading) return;

            var favorite = this.SelectedFavorite;
            var zip = this.SelectedCity;
            var ordering = this.SelectedOrdering;

            await GetGroups(favorite.TopicId, zip, favorite.CategoryId, false, ordering);
        }

        public async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var category = this.SelectedCategory;
            var zip = this.SelectedCity;
            var ordering = this.SelectedOrdering;
            var topicId = this.SelectedTopic;

            await GetGroups(topicId, zip, category, false, ordering);
        }

        public async void OnExport(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            string line = $"Group Name;City;Meetup Name;Url;Registered;Date";
            sb.AppendLine(line);

            foreach (var item in Groups)
            {
                if (item.AllEvents != null)
                {
                    foreach (var meetup in item.AllEvents)
                    {
                        line = $"{item?.Name};{item?.City};{meetup?.Name};{meetup?.EventUrl};{meetup?.YesRsvpCount};{meetup?.Time}";
                        sb.AppendLine(line);
                    }
                }
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
    }
}
