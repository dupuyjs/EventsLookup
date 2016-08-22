namespace EventsLookup.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
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
        private ObservableCollection<Favorite> _favorites = null;
        private Dictionary<string, string> _ordering = null;
        private string _selectedCity = string.Empty;
        private int _selectedCategory = default(int);
        private int _selectedTopic = default(int);
        private Favorite _selectedFavorite = null;
        private string _selectedOrdering = null;
        private int _groupsCount = default(int);
        private string _keywords = "android";
        private Dictionary<int, List<Event>> _cacheManager = null;
        private IconElement _favoriteIcon = new SymbolIcon(Symbol.UnFavorite);

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetupViewModel"/> class.
        /// </summary>
        public MeetupViewModel()
        {
            IsDataLoaded = false;
            IsUserAuthenticated = false;
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
                    return _meetupProxy = MeetupClientFactory.CreateMeetupClient();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether data is loaded.
        /// </summary>
        public bool IsDataLoaded { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether user is authenticated.
        /// </summary>
        public bool IsUserAuthenticated { get; set; }

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
        public ObservableCollection<Favorite> Favorites
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
        /// Gets or sets temporary created Favorite
        /// </summary>
        public Favorite TemporaryFavorite { get; set; }

        /// <summary>
        /// Gets or sets Favorite Icon
        /// </summary>
        public IconElement FavoriteIcon
        {
            get
            {
                return _favoriteIcon;
            }

            set
            {
                if (_favoriteIcon != value)
                {
                    _favoriteIcon = value;
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        RaisePropertyChanged(() => FavoriteIcon);
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
        /// Gets or sets the Cache Manager.
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

        /// <summary>
        /// Initialize default data.
        /// </summary>
        /// <returns>Void.</returns>
        public async Task Initialize()
        {
            try
            {
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

                SelectedCity = Cities.First().Zip;
                SelectedCategory = 34;
                SelectedOrdering = OrderingEnum.MostActive.ToFriendlyString();

                SelectedFavorite = this.Favorites.First();
            }
            catch (Exception)
            {
                DialogService.DisplayError(ResourceHelper.GetErrorString("Network/Title"), ResourceHelper.GetErrorString("Network/Content"), string.Empty);
            }
        }

        #endregion

        /// <summary>
        /// Main loading method. Gets group and events from Meetup API platform.
        /// </summary>
        /// <param name="topicId">The topic ID.</param>
        /// <param name="zip">The zip code.</param>
        /// <param name="categoryId">The category ID.</param>
        /// <param name="upcomingOnly">Filter groups with upcoming events only.</param>
        /// <param name="ordering">Ordering enumeration.</param>
        /// <returns>Boolean Value. True if load completed succesfully.</returns>
        public async Task<bool> GetGroups(int topicId, string zip, int categoryId, bool upcomingOnly, OrderingEnum ordering)
        {
            bool isOperationCompleted = false;
            IsLoading = true;

            try
            {
                List<Event> meetups = new List<Event>();
                var groups = await MeetupProxy.GetGroupsAsync(topicId, zip, categoryId, upcomingOnly, ordering);

                Groups = groups.Results;
                GroupsCount = groups.TotalCount;

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
                            var response = await MeetupProxy.GetEventsAsync(item.Id);
                            events = response.Results;
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

                isOperationCompleted = true;
            }
            catch (Exception)
            {
                DialogService.DisplayError(ResourceHelper.GetErrorString("Network/Title"), ResourceHelper.GetErrorString("Network/Content"), string.Empty);
            }

            IsDataLoaded = true;
            IsLoading = false;

            return isOperationCompleted;
        }

        /// <summary>
        /// Event Handler - Query Topics when Keywords added.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="args">Arguments.</param>
        public async void OnKeywordsQuery(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            try
            {
                var topics = await MeetupProxy.GetTopicsAsync(Keywords);
                Topics = topics;
                SelectedTopic = topics.First().Id;
            }
            catch (Exception)
            {
                DialogService.DisplayError(ResourceHelper.GetErrorString("Network/Title"), ResourceHelper.GetErrorString("Network/Content"), string.Empty);
            }
        }

        /// <summary>
        /// Event Handler - Load Data when Favorite is selected.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">Arguments.</param>
        public async void OnFavoriteChanged(object sender, SelectionChangedEventArgs e)
        {
            var favorite = SelectedFavorite;
            var zip = SelectedCity;
            var ordering = SelectedOrdering.ToOrdering();

            if (favorite != null)
            {
                TemporaryFavorite = null;
                FavoriteIcon = new SymbolIcon(Symbol.UnFavorite);
                await GetGroups(favorite.TopicId, zip, favorite.CategoryId, false, ordering);
            }
        }

        /// <summary>
        /// Event Handler - Add or Remove Favorite.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">Arguments.</param>
        public void OnAddRemoveFavorite(object sender, RoutedEventArgs e)
        {
            if (TemporaryFavorite == null && SelectedFavorite != null)
            {
                TemporaryFavorite = SelectedFavorite;
                Favorites.Remove(SelectedFavorite);
                FavoriteIcon = new SymbolIcon(Symbol.Favorite);
            }
            else
            {
                if (TemporaryFavorite != null)
                {
                    Favorites.AddSorted<Favorite>(TemporaryFavorite, Favorite.SortDisplayNameDescending());
                    SelectedFavorite = TemporaryFavorite;
                }
            }
        }

        /// <summary>
        /// Event Handler - Load Data when Filter (City or Ordering) is selected.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">Arguments.</param>
        public async void OnFilterChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsDataLoaded)
            {
                return;
            }

            var favorite = SelectedFavorite;
            var zip = SelectedCity;
            var ordering = SelectedOrdering.ToOrdering();

            if (favorite != null)
            {
                await GetGroups(favorite.TopicId, zip, favorite.CategoryId, false, ordering);
            }
        }

        /// <summary>
        /// Event Handler - Load Data when a specific Search is performed.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">Arguments.</param>
        public async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var category = SelectedCategory;
            var zip = SelectedCity;
            var ordering = SelectedOrdering.ToOrdering();
            var topicId = SelectedTopic;

            var query = from item in Topics
                        where item.Id == topicId
                        select item;

            TemporaryFavorite = new Favorite(query.First().Name.ToUpper(), SelectedCategory, SelectedTopic);
            FavoriteIcon = new SymbolIcon(Symbol.Favorite);

            await GetGroups(topicId, zip, category, false, ordering);
        }

        /// <summary>
        /// Event Handler - Exports Data to CSV format.
        /// </summary>
        /// <param name="sender">The sender control.</param>
        /// <param name="e">Arguments.</param>
        public async void OnExport(object sender, RoutedEventArgs e)
        {
            var delimiter = CultureInfo.CurrentCulture.TextInfo.ListSeparator[0];

            StringBuilder sb = new StringBuilder();

            string line = $"Date{delimiter}Group Name{delimiter}Meetup Name{delimiter}Venue Name{delimiter}Venue City{delimiter}Event Url{delimiter}YesRsvpCount";
            sb.AppendLine(line);

            foreach (var monthGroup in Meetups)
            {
                sb.AppendLine();
                sb.AppendLine(string.Format("{0:MMMM yyyy}", monthGroup.Key).ToUpper());
                sb.AppendLine();
                foreach (var meetup in monthGroup)
                {
                    line = $"{string.Format("{0:d}", meetup?.Time)}{delimiter}{meetup?.Group?.Name.Replace(delimiter, '|')}{delimiter}{meetup?.Name.Replace(delimiter, '|')}{delimiter}{meetup?.Venue?.Name}{delimiter}{meetup?.Venue?.City}{delimiter}{meetup?.EventUrl}{delimiter}{meetup?.YesRsvpCount}";
                    sb.AppendLine(line);
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
