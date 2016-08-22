namespace EventsLookup.ViewModels
{
    using GalaSoft.MvvmLight.Ioc;
    using Microsoft.Practices.ServiceLocation;

    /// <summary>
    /// ViewModelLocator
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MeetupViewModel>();
        }

        /// <summary>
        /// Gets MeetupViewModel default instance.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public MeetupViewModel Meetup => ServiceLocator.Current.GetInstance<MeetupViewModel>();
    }
}
