namespace EventsLookup.Views
{
    using Windows.ApplicationModel;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// About Page.
    /// </summary>
    public sealed partial class AboutPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutPage"/> class.
        /// </summary>
        public AboutPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var packageVersion = Package.Current.Id.Version;
            Version.Text = $"{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}";
        }
    }
}
