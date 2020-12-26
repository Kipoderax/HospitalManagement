using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The current users first name
        /// </summary>
        public TextEntryViewModel FirstName { get; set; }

        /// <summary>
        /// The current users last name
        /// </summary>
        public TextEntryViewModel LastName { get; set; }

        /// <summary>
        /// The current users identify as login
        /// </summary>
        public TextEntryViewModel Identify { get; set; }

        /// <summary>
        /// The current users type
        /// </summary>
        public TextEntryViewModel Type { get; set; }

        /// <summary>
        /// The current users specialize
        /// </summary>
        public TextEntryViewModel Specialize{ get; set; }

        /// <summary>
        /// The current users pwd number
        /// </summary>
        public TextEntryViewModel PwdNumber { get; set; }

        /// <summary>
        /// The current users mask password
        /// </summary>
        public PasswordEntryViewModel Password { get; set; }

        /// <summary>
        /// The text for the logout button
        /// </summary>
        public string LogoutButtonText { get; set; }

        /// <summary>
        /// The text for the new employee button
        /// NOTE: It's visible for users login as administrator
        /// </summary>
        public string NewEmployeeText { get; set; }

        /// <summary>
        /// The text for the duty button
        /// </summary>
        public string AddDutyButtonText { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to open the settings menu
        /// </summary>
        public ICommand OpenCommand { get; set; }

        /// <summary>
        /// The command to close the settings menu
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to logout of the application
        /// </summary>
        public ICommand LogoutCommand { get; set; }

        /// <summary>
        /// The command to clear the users data from the view model
        /// </summary>
        public ICommand ClearUserDataCommand { get; set; }

        /// <summary>
        /// The command to open employee forms for register to application
        /// </summary>
        public ICommand NewEmployeeCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel ()
        {
            // Create commands
            OpenCommand = new RelayCommand( Open );
            CloseCommand = new RelayCommand( Close );
            LogoutCommand = new RelayCommand( Logout );
            ClearUserDataCommand = new RelayCommand( ClearUserData );
            NewEmployeeCommand = new RelayCommand( NewEmployee );

            // TODO: Get from localization
            LogoutButtonText = "Wyloguj";
            AddDutyButtonText = "Dodaj dyżur";
            NewEmployeeText = "Nowy pracownik";
        }

        #endregion

        /// <summary>
        /// Closes the settings menu
        /// </summary>
        private void Close ()
        {
            // Close register form employee if is open
            if (IoC.Application.NewEmployeeFormVisible)
                IoC.Application.NewEmployeeFormVisible = false;

            // Otherwise close settings menu
            else
                IoC.Application.SettingsMenuVisible = false;
        }

        /// <summary>
        /// Open the settings menu
        /// </summary>
        private void Open ()
        {
            // Close settings menu
            IoC.Application.SettingsMenuVisible = true;
        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        private void Logout ()
        {
            // TODO: Confirm the user wants to logout

            // Clean all application level view models that contain
            // any information about the current user
            ClearUserData();

            // Go to login page
            IoC.Application.GoToPage( ApplicationPage.Login );
        }

        /// <summary>
        /// Clears any data specific to the current user
        /// </summary>
        private void ClearUserData()
        {
            // Clear all view models containing the users info
            FirstName = null;
            LastName = null;
            Identify = null;
            Type = null;
            Specialize = null;
            PwdNumber = null;
            Password = null;
        }

        private void NewEmployee()
        {
            IoC.Application.NewEmployeeFormVisible = true;
        }
    }
}
