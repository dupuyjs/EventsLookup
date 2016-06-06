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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace EventsLookup.ViewModels
{
    public class MeetupViewModel : ViewModelBase
    {
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

        private IMeetupClient _meetupProxy = null;
        public IMeetupClient MeetupProxy
        {
            get
            {
                return this._meetupProxy ?? (_meetupProxy = MeetupClientFactory.CreateMeetupClient(Keys.MeetupApiKey));
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

        private bool? _upcoming = false;
        public bool? Upcoming
        {
            get
            {
                return _upcoming;
            }
            set
            {
                _upcoming = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => Upcoming);
                });
            }
        }

        private int _groupsNumber = 0;
        public int GroupsNumber
        {
            get
            {
                return _groupsNumber;
            }
            set
            {
                _groupsNumber = value;
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    RaisePropertyChanged(() => GroupsNumber);
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

        public MeetupViewModel()
        {
            Task.Run(() => Initialize());
        }

        #region Initialization

        public async Task<bool> Initialize()
        {
            try
            {
                this.Favorites = Favorite.GetDefaultTopics();

                var cities = await MeetupProxy.GetCities();
                this.Cities = cities.Results;

                var categories = await MeetupProxy.GetCategories();
                this.Categories = categories.Results;

                var ordering = MeetupLibrary.Models.Ordering.GetItems();
                this.Ordering = ordering;

                this.SelectedCity = "meetup1";
                this.SelectedCategory = 34;
                this.SelectedOrdering = "most_active";

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
                var groups = await MeetupProxy.GetGroups(topicId, zip, categoryId, upcomingOnly, ordering);
                this.Groups = groups;
                this.GroupsNumber = groups.Count();

                foreach (var item in Groups)
                {
                    if (item.NextEvent != null)
                    {
                        var events = await MeetupProxy.GetEvents(item.Id);
                        item.AllEvents = events.Results;
                    }
                }
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
            var favorite = this.SelectedFavorite;
            var zip = this.SelectedCity;
            var ordering = this.SelectedOrdering;
            var upcomingOnly = this.Upcoming.Value;

            await GetGroups(favorite.TopicId, zip, favorite.CategoryId, upcomingOnly, ordering);
        }

        public async void OnSubmit(object sender, RoutedEventArgs e)
        {
            var category = this.SelectedCategory;
            var zip = this.SelectedCity;
            var ordering = this.SelectedOrdering;
            var topicId = this.SelectedTopic;
            var upcomingOnly = this.Upcoming.Value;

            await GetGroups(topicId, zip, category, upcomingOnly, ordering);
        }
    }
}
