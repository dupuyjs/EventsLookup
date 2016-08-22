namespace EventsLookup.Helpers
{
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Attached Property for WebView control.
    /// </summary>
    public class HtmlStringExtension
    {
        /// <summary>
        /// HtmlString Property - Add navigation to a specific Html string source.
        /// </summary>
        public static readonly DependencyProperty HtmlStringProperty =
           DependencyProperty.RegisterAttached("HtmlString", typeof(string), typeof(HtmlStringExtension), new PropertyMetadata(string.Empty, OnHtmlStringChanged));

        /// <summary>
        /// Gets Html string source.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <returns>The Html string.</returns>
        public static string GetHtmlString(DependencyObject obj)
        {
            return (string)obj.GetValue(HtmlStringProperty);
        }

        /// <summary>
        /// Sets Html string source.
        /// </summary>
        /// <param name="obj">The dependency object.</param>
        /// <param name="value">The Html string.</param>
        public static void SetHtmlString(DependencyObject obj, string value)
        {
            obj.SetValue(HtmlStringProperty, value);
        }

        private static void OnHtmlStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView webView = d as WebView;
            if (webView != null)
            {
                webView.NavigateToString((string)e.NewValue);
            }
        }
    }
}
