// ******************************************************************
// THE CODE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH
// THE CODE OR THE USE OR OTHER DEALINGS IN THE CODE.
// ******************************************************************

namespace EventsLookup.Views
{
    using System;
    using System.Collections.Generic;
    using EventsLookup.Models.Navigation;
    using EventsLookup.Services.DialogService;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Ioc;
    using GalaSoft.MvvmLight.Threading;
    using Windows.ApplicationModel.Core;
    using Windows.Foundation;
    using Windows.UI;
    using Windows.UI.Core;
    using Windows.UI.Popups;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;

    /// <summary>
    /// Core shell page, implements basic layout.
    /// </summary>
    public sealed partial class ShellPage : Page, IDialogService
    {
        private Frame _contentFrame;
        private RelayCommand<NavType> _navCommand;
        private RelayCommand _burgerCommand;
        private RelayCommand _backCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellPage"/> class.
        /// </summary>
        /// <param name="frame">A frame object.</param>
        public ShellPage(Frame frame)
        {
            InitializeComponent();
            ShellSplitView.Content = frame;
            _contentFrame = frame;

            DispatcherHelper.Initialize();
            SimpleIoc.Default.Register<IDialogService>(() => this);

            Loaded += (s, e) => SetLayoutPreference();
            Loaded += (s, e) => UpdateNavigation();
            _contentFrame.Navigated += (s, e) => UpdateNavigation();

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
        }

        /// <summary>
        /// Gets relay command to navigate to a specific page.
        /// </summary>
        public RelayCommand<NavType> NavCommand
        {
            get
            {
                return _navCommand ?? (_navCommand = new RelayCommand<NavType>(ExecuteNavigation));
            }
        }

        /// <summary>
        /// Gets relay command to open or close menu pane.
        /// </summary>
        public RelayCommand BurgerCommand
        {
            get
            {
                return _burgerCommand ?? (_burgerCommand = new RelayCommand(ExecuteMenu));
            }
        }

        /// <summary>
        /// Gets relay command to manage back button.
        /// </summary>
        public RelayCommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand(ExecuteBack));
            }
        }

        void IDialogService.DisplayStatus(string message, string title, string buttonText)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                var dialog = new MessageDialog(message, title);
                await dialog.ShowAsync();
            });
        }

        void IDialogService.DisplayError(string errorMessage, string title, string buttonText)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(async () =>
            {
                var dialog = new MessageDialog(errorMessage, title);
                await dialog.ShowAsync();
            });
        }

        #region Menu Commands

        private void ExecuteNavigation(NavType navType)
        {
            var type = navType.Type;
            this._contentFrame.Navigate(navType.Type, this);
        }

        private void ExecuteMenu()
        {
            this.ShellSplitView.IsPaneOpen = !this.ShellSplitView.IsPaneOpen;
        }

        private void ExecuteBack()
        {
            this._contentFrame.GoBack();
        }

        private void DontCheckMe(object s, RoutedEventArgs e)
        {
            (s as RadioButton).IsChecked = false;
        }

        #endregion

        #region Back button navigation

        private void UpdateNavigation()
        {
            // Update radiobuttons after frame navigates
            var type = _contentFrame.CurrentSourcePageType;

            foreach (var radioButton in AllRadioButtons(this))
            {
                var target = radioButton.CommandParameter as NavType;
                if (target == null)
                {
                    continue;
                }

                radioButton.IsChecked = target.Type.Equals(type);
            }

            // Update back button
            if (this._contentFrame.CanGoBack)
            {
                BackButton.IsEnabled = true;
            }
            else
            {
                BackButton.IsEnabled = false;
            }
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (this._contentFrame == null)
            {
                return;
            }

            // Navigate back if possible, and if the event has not
            // already been handled .
            if (this._contentFrame.CanGoBack && e.Handled == false)
            {
                e.Handled = true;
                this._contentFrame.GoBack();
            }
        }

        private List<RadioButton> AllRadioButtons(DependencyObject parent)
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

        #region Layout Preference

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

        #endregion
    }
}
