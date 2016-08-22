namespace EventsLookup.Views
{
    using EventsLookup.ViewModels;
    using Microsoft.Practices.ServiceLocation;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Settings Page.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// /// </summary>
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Gets MeetupViewModel default instance.
        /// </summary>
        public MeetupViewModel Default
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MeetupViewModel>();
            }
        }
    }
}
