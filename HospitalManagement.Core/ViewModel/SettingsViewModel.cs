using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The settings state as a view model
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
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
