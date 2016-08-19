namespace EventsLookup.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EventsLookup.Helpers;
    using EventsLookup.Models;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Threading;
    using MeetupLibrary;
    using MeetupLibrary.Models;
    using Microsoft.Practices.ServiceLocation;
    using Windows.ApplicationModel.Resources;
    using Windows.Storage;
    using Windows.Storage.Pickers;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Main View Model
    /// </summary>
    public class MeetupViewModel : ViewModelBase
    {
        private IMeetupClient _meetupProxy = null;
        private bool _isLoading = true;
        private IOrderedEnumerable<IGrouping<DateTime, Event>> _meetups = null;
        private Member _user = null;
        private List<Group> _groups = null;
        private List<Event> _userCalendar = null;
        private List<City> _cities = null;
        private List<Category> _categories = null;
        private List<Topic> _topics = null;
        private List<Favorite> _favorites = null;
        private Dictionary<string, string> _ordering = null;
        private string _selectedCity = string.Empty;
        private int _selectedCategory = default(int);
        private int _selectedTopic = default(int);
        private Favorite _selectedFavorite = null;
        private string _selectedOrdering = null;
        private int _groupsCount = default(int);
        private string _keywords = "android";
        private Dictionary<int, List<Event>> _cacheManager = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetupViewModel"/> class.
        /// </summary>
        public MeetupViewModel()
        {
        }

        #region Properties

        /// <summary>
        /// Gets proxy to access Meetup platform REST API.
        /// </summary>
        public IMeetupClient MeetupProxy
        {
            get
            {
                if (this._meetupProxy != null)
                {
                    return this._meetupProxy;
                }
                else
                {
                    return _meetupProxy = MeetupClientFactory.CreateMeetupClient(Keys.MeetupApiKey);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether data is loaded.
        /// </summary>
        public bool IsDataLoaded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether data is loading.
        /// </summary>
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

        /// <summary>
        /// Gets or sets list of meetup groups.
        /// </summary>
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

        /// <summary>
        /// Gets or sets list of meetup events.
        /// </summary>
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

        /// <summary>
        /// Gets or sets list of meetup events (specific to authenticated user).
        /// </summary>
        public List<Event> UserCalendar
        {
            get
            {
                return _userCalendar;
            }

            set
            {
                _userCalendar = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => UserCalendar);
                });
            }
        }

        /// <summary>
        /// Gets or sets authenticated user profile.
        /// </summary>
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

        /// <summary>
        /// Gets or sets list of meetup cities.
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected meetup city.
        /// </summary>
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

        /// <summary>
        /// Gets or sets list of meetup categories.
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected meetup category.
        /// </summary>
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

        /// <summary>
        /// Gets or sets ordering options.
        /// </summary>
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


        /// <summary>
        /// Gets or sets selected ordering option.
        /// </summary>
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

        /// <summary>
        /// Gets or sets meetup topics.
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected meetup topic.
        /// </summary>
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

        /// <summary>
        /// Gets or sets favorites.
        /// </summary>
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

        /// <summary>
        /// Gets or sets selected favorite.
        /// </summary>
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

        /// <summary>
        /// Gets or sets total items in meetup groups list.
        /// </summary>
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

        /// <summary>
        /// Gets or sets keywords.
        /// </summary>
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

        /// <summary>
        /// Gets dialog service instance.
        /// </summary>
        public Services.DialogService.IDialogService DialogService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<Services.DialogService.IDialogService>();
            }
        }

        #endregion

        #region Caching

        /// <summary>
        /// Cache Manager
        /// </summary>
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

        #region Initialization

        public bool IsValidOAuthKey()
        {
            if (Keys.MeetupApiKey.Equals("YourApiKey"))
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                string key = (string)localSettings.Values["MeetupApiKey"];

                if (key == null || key.Equals("YourApiKey"))
                {
                    return false;
                }
                else
                {
                    Keys.MeetupApiKey = key;
                }
            }

            return true;
        }

        public async Task Initialize()
        {
            if (!IsValidOAuthKey())
            {
                var loader = ResourceLoader.GetForCurrentView("Errors");
                DialogService.DisplayError(loader.GetString("Key/Message"), loader.GetString("Key/Caption"), "");
                //return false;
            }

            try
            {
                //DialogService.DisplayError(ResourceHelper.GetErrorString("Key/Message"), ResourceHelper.GetErrorString("Key /Caption"), "");

                Favorites = Favorite.GetDefaultFavorites();

                var member = await MeetupProxy.GetUserProfileAsync();
                User = member;

                var calendar = await MeetupProxy.GetUserCalendarAsync(member.Id);
                UserCalendar = calendar.Results;

                var cities = await MeetupProxy.GetCitiesAsync();
                Cities = cities.Results;

                var categories = await MeetupProxy.GetCategoriesAsync();
                Categories = categories.Results;

                var ordering = MeetupLibrary.Models.Ordering.GetItems();
                Ordering = ordering;

                SelectedCity = "meetup1";
                SelectedCategory = 34;
                SelectedOrdering = OrderingEnum.MostActive.ToFriendlyString();

                this.SelectedFavorite = this.Favorites.First();
            }
            catch (Exception)
            {
                DialogService.DisplayError(ResourceHelper.GetErrorString("Key/Message"), ResourceHelper.GetErrorString("Key /Caption"), "");
            }

            IsDataLoaded = true;
            //return true;
        }

        #endregion

        #region Get Topics

        public async void OnKeywordsQuery(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                var topics = await MeetupProxy.GetTopicsAsync(Keywords);
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

        public async Task<bool> GetGroups(int topicId, string zip, int categoryId, bool upcomingOnly, OrderingEnum ordering)
        {
            try
            {
                List<Event> meetups = new List<Event>();
                var groups = await MeetupProxy.GetGroupsAsync(topicId, zip, categoryId, upcomingOnly, ordering);

                Groups = groups.Results;
                GroupsCount = groups.TotalCount;

                //IsGroupsEmpty = (GroupsCount == 0) ? true : false;

                foreach (var item in Groups)
                {
                    if (item.AllEvents == null && item.NextEvent != null)
                    {
                        List<Event> events = null;

                        if (CacheManager.ContainsKey(item.Id))
                        {
                            if (CacheManager[item.Id] != null)
                            {
                                events = CacheManager[item.Id];
                            }
                        }
                        else
                        {
                            events = (await MeetupProxy.GetEventsAsync(item.Id)).Results;
                            CacheManager.Add(item.Id, events);
                        }

                        item.AllEvents = events;
                        meetups.AddRange(events);
                    }
                }

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
            var ordering = this.SelectedOrdering.ToOrdering();

            if (favorite != null)
            {
                await GetGroups(favorite.TopicId, zip, favorite.CategoryId, false, ordering);
            }

            this.IsLoading = false;
        }

        public async void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoading)
            {
                return;
            }

            var favorite = this.SelectedFavorite;
            var zip = this.SelectedCity;
            var ordering = this.SelectedOrdering.ToOrdering();

            if (favorite != null)
            {
                await GetGroups(favorite.TopicId, zip, favorite.CategoryId, false, ordering);
            }
        }

        public async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var category = this.SelectedCategory;
            var zip = this.SelectedCity;
            var ordering = this.SelectedOrdering.ToOrdering();
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
