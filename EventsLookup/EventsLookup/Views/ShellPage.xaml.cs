using EventsLookup.Models.Navigation;
using EventsLookup.Services.DialogService;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EventsLookup.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShellPage : Page, IDialogService
    {
        private Frame _contentFrame;

        #region Commands

        private RelayCommand<NavType> _navCommand;
        public RelayCommand<NavType> NavCommand { get { return _navCommand ?? (_navCommand = new RelayCommand<NavType>(ExecuteNavigation)); } }

        private void ExecuteNavigation(NavType navType)
        {
            var type = navType.Type;
            this._contentFrame.Navigate(navType.Type, this);
        }

        private RelayCommand _burgerCommand;
        public RelayCommand BurgerCommand { get { return _burgerCommand ?? (_burgerCommand = new RelayCommand(ExecuteMenu)); } }

        private void ExecuteMenu()
        {
            this.ShellSplitView.IsPaneOpen = !this.ShellSplitView.IsPaneOpen;
        }

        private RelayCommand _backCommand;
        public RelayCommand BackCommand { get { return _backCommand ?? (_backCommand = new RelayCommand(ExecuteBack)); } }

        private void ExecuteBack()
        {
            this._contentFrame.GoBack();
        }

        #endregion

        public ShellPage(Frame frame)
        {
            this.InitializeComponent();
            this.ShellSplitView.Content = frame;
            this._contentFrame = frame;

            DispatcherHelper.Initialize();
            SimpleIoc.Default.Register<IDialogService>(() => this);

            //var update = new Action(() => UpdateNavigation());

            this.Loaded += (s, e) => SetLayoutPreference();
            this.Loaded += (s, e) => UpdateNavigation();
            this._contentFrame.Navigated += (s, e) => UpdateNavigation();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        #region Back button navigation

        private void UpdateNavigation()
        {
            //if (this._contentFrame == null)
            //    return;

            // Update radiobuttons after frame navigates
            var type = this._contentFrame.CurrentSourcePageType;
            foreach (var radioButton in AllRadioButtons(this))
            {
                var target = radioButton.CommandParameter as NavType;
                if (target == null)
                    continue;
                radioButton.IsChecked = target.Type.Equals(type);
            }

            // Update back button
            if (this._contentFrame.CanGoBack)
            {
                //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                BackButton.IsEnabled = true;
            }
            else
            {
                BackButton.IsEnabled = false;
                //SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this._contentFrame == null)
                return;

            // Navigate back if possible, and if the event has not 
            // already been handled .
            if (this._contentFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                this._contentFrame.GoBack();
            }
        }

        public List<RadioButton> AllRadioButtons(DependencyObject parent)
        {
            var list = new List<RadioButton>();
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is RadioButton)
                {
                    list.Add(child as RadioButton);
                    continue;
                }
                list.AddRange(AllRadioButtons(child));
            }
            return list;
        }

        #endregion

        private void SetLayoutPreference()
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(320, 320));

            ApplicationView.GetForCurrentView().TitleBar.BackgroundColor = Colors.Black;
            ApplicationView.GetForCurrentView().TitleBar.ForegroundColor = Colors.White;

            ApplicationView.GetForCurrentView().TitleBar.InactiveBackgroundColor = Colors.Black;
            ApplicationView.GetForCurrentView().TitleBar.InactiveForegroundColor = Colors.White;

            ApplicationView.GetForCurrentView().TitleBar.ButtonBackgroundColor = Colors.Black;
            ApplicationView.GetForCurrentView().TitleBar.ButtonForegroundColor = Colors.White;

            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveBackgroundColor = Colors.Black;
            ApplicationView.GetForCurrentView().TitleBar.ButtonInactiveForegroundColor = Colors.White;

            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            Chrome.Height = coreTitleBar.Height;
            Window.Current.SetTitleBar(TitleBar);

            coreTitleBar.LayoutMetricsChanged += (s, e) =>
            {
                Chrome.Height = s.Height;
                RightMask.Width = s.SystemOverlayRightInset;
            };

            coreTitleBar.IsVisibleChanged += (s, e) =>
            {
                Chrome.Visibility = coreTitleBar.IsVisible ? Visibility.Visible : Visibility.Collapsed;
            };
        }

        private void DontCheckMe(object s, RoutedEventArgs e)
        {
            (s as RadioButton).IsChecked = false;
        }

        void IDialogService.DisplayStatus(string message, string title, string buttonText)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () => {
                var dialog = new MessageDialog(message, title);
                await dialog.ShowAsync();
            });
        }

        void IDialogService.DisplayError(string errorMessage, string title, string buttonText)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () => {
                var dialog = new MessageDialog(errorMessage, title);
                await dialog.ShowAsync();
            });
        }
    }
}
