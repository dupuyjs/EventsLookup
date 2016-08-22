namespace EventsLookup.Controls
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media.Animation;

    /// <summary>
    /// Custom Progress Logo Control
    /// </summary>
    public sealed partial class ProgressLogo : UserControl
    {
        /// <summary>
        /// Dependency Property associated with IsActive
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(ProgressLogo), new PropertyMetadata(false, OnChanged));

        private static Storyboard _animation = new Storyboard();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressLogo"/> class.
        /// </summary>
        public ProgressLogo()
        {
            this.InitializeComponent();
            _animation = AnimateRing;
        }

        /// <summary>
        /// Gets or sets a value indicating whether Progress Ring is active.
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue)
            {
                _animation.Begin();
            }
            else
            {
                _animation.Stop();
            }
        }
    }
}
