namespace HospitalManagement.Core
{
    /// <summary>
    /// The application state as the view model
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Login;

        /// <summary>
        /// The view model to use for the current page when the CurrentPage changes
        /// NOTE: This is not a live up-to-date view model of the current page
        ///       it is simply used to sed the view model of the current page
        ///       at the time it changes
        /// </summary>
        public BaseViewModel CurrentPageViewModel { get; set; }

        /// <summary>
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; }

        /// <summary>
        /// True if the settings menu should be shown
        /// </summary>
        public bool SettingsMenuVisible { get; set; }

        /// <summary>
        /// True if user as administrator want add new employee
        /// </summary>
        public bool NewEmployeeFormVisible { get; set; }

        #endregion

        /// <summary>
        /// Navigate to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage ( ApplicationPage page, BaseViewModel viewModel = null )
        {
            // Always hide settings page if we are changing pages
            SettingsMenuVisible = false;
            NewEmployeeFormVisible = false;

            // Set the view model
            CurrentPageViewModel = viewModel;

            // Set the current page
            CurrentPage = page;
            
            // Fire off a CurrentPage changed event
            OnPropertyChanged ( nameof(CurrentPage) );

            // Show side menu or not
            SideMenuVisible = page == ApplicationPage.Work;
        }
    }
}
