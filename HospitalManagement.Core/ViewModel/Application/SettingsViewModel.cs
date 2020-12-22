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
        public TextEntryViewModel Password { get; set; }

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

            // TODO: Remove this with real information pulled from our database in future
            FirstName = new TextEntryViewModel { Label = "Imię", OriginalText = "Jessica" };
            LastName = new TextEntryViewModel { Label = "Nazwisko", OriginalText = "Stalon" };
            Identify = new TextEntryViewModel { Label = "Identyfikator", OriginalText = "JS12321" };
            Type = new TextEntryViewModel { Label = "Posada", OriginalText = "Lekarz" };
            Specialize = new TextEntryViewModel { Label = "Specjalizacja", OriginalText = "Urolog" };
            PwdNumber = new TextEntryViewModel { Label = "Numer PWD", OriginalText = "pwd135" };
            Password = new TextEntryViewModel { Label = "Hasło", OriginalText = "********" };
        }

        #endregion

        /// <summary>
        /// Closes the settings menu
        /// </summary>
        private void Close ()
        {
            // Close settings menu
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

    }
}
