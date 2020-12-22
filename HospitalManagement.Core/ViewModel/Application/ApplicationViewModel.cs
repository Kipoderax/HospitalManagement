using System;

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
        /// True if the side menu should be shown
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// True if the settings menu should be shown
        /// </summary>
        public bool SettingsMenuVisible { get; set; } = false;

        #endregion

        /// <summary>
        /// Navigate to the specified page
        /// </summary>
        /// <param name="page">The page to go to</param>
        public void GoToPage ( ApplicationPage page)
        {
            // Set the current page
            CurrentPage = page;

            // Show side menu or not
            SideMenuVisible = page == ApplicationPage.Work;
        }

    }
}
