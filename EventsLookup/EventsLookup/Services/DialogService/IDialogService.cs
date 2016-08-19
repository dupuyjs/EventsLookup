namespace EventsLookup.Services.DialogService
{
    public interface IDialogService
    {
        void DisplayStatus(string message, string title, string buttonText);

        void DisplayError(string errorMessage, string title, string buttonText);
    }
}
