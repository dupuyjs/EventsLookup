namespace EventsLookup.Services.DialogService
{
    /// <summary>
    /// IDialogService interface
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Display Status Message
        /// </summary>
        /// <param name="title">A message title.</param>
        /// <param name="content">A message content.</param>
        /// <param name="buttonText">Text displayed on button.</param>
        void DisplayStatus(string title, string content, string buttonText);

        /// <summary>
        /// Display Error Message
        /// </summary>
        /// <param name="title">A message title.</param>
        /// <param name="content">A message content.</param>
        /// <param name="buttonText">Text displayed on button.</param>
        void DisplayError(string title, string content, string buttonText);
    }
}
