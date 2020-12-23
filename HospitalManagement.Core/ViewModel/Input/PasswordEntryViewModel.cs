using System.Security;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The view model for a password entry to edit a password
    /// </summary>
    public class PasswordEntryViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The label to identify what this value is for
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The fake password display string
        /// </summary>
        public string FakePassword { get; set; }

        /// <summary>
        /// The current password hint text
        /// </summary>
        public string CurrentPasswordHintText { get; set; }

        /// <summary>
        /// The new password hint text
        /// </summary>
        public string NewPasswordHintText { get; set; }

        /// <summary>
        /// The confirm password hint text
        /// </summary>
        public string ConfirmPasswordHintText { get; set; }

        /// <summary>
        /// The current saved password
        /// </summary>
        public SecureString CurrentPassword { get; set; }

        /// <summary>
        /// The current non-commit edited password
        /// </summary>
        public SecureString NewPassword { get; set; }

        /// <summary>
        /// The current non-commit confirmed password
        /// </summary>
        public SecureString ConfirmPassword { get; set; }

        /// <summary>
        /// Indicates if the current text is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        /// <summary>
        /// True if input password is correct, otherwise false
        /// </summary>
        public bool IsNotCorrect { get; set; }

        /// <summary>
        /// Inform user about incorrect input password
        /// </summary>
        public string BadRequestMessage { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        public ICommand EditCommand { get; set; }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        public ICommand CancelCommand { get; set; }

        /// <summary>
        /// Commits the edits and saves the value
        /// as well as goes back to non-edit mode
        /// </summary>
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public PasswordEntryViewModel ()
        {
            // Create commands
            EditCommand = new RelayCommand( Edit );
            CancelCommand = new RelayCommand( Cancel );
            SaveCommand = new RelayCommand( Save );

            // Set default hints
            // TODO: Replace with localization text
            CurrentPasswordHintText = "Aktualne hasło";
            NewPasswordHintText = "Nowe hasło";
            ConfirmPasswordHintText = "Powtórz hasło";
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Commits the content and exits out of edit mode
        /// </summary>
        private void Save ()
        {
            // TODO: Save content
            // Make sure current password is correct
            // TODO: This will come from the real back-end store of this users password
            //               or via asking the web server to confirm it
            var storedPassword = "Testing";

            // Confirm current password is a match
            // NOTE: Typically this isn't done here, it's done on the server
            if (storedPassword != CurrentPassword.UnSecure())
            {
                IsNotCorrect = true;
                // Let user know 
                BadRequestMessage = "Nie prawidłowe hasło";

                return;
            }

            // Now check that the new and confirm password match
            if (NewPassword.UnSecure() != ConfirmPassword.UnSecure())
            {
                IsNotCorrect = true;
                //Let user know
                BadRequestMessage = "Wprowadzone hasła się nie zgadzają";

                return;
            }

            // Check we actually have a password 
            if (NewPassword.UnSecure().Length == 0)
            {
                IsNotCorrect = true;
                //Let user know
                BadRequestMessage = "Hasło jest za krótkie";

                return;
            }

            // Set the edited password to the current value
            CurrentPassword = new SecureString();
            foreach (var c in NewPassword.UnSecure().ToCharArray())
                CurrentPassword.AppendChar( c );

            Editing = false;
            IsNotCorrect = false;
        }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        private void Cancel ()
        {
            Editing = false;
            IsNotCorrect = false;
        }

        /// <summary>
        /// Puts the control into edit mode
        /// </summary>
        public void Edit ()
        {
            // Clear all password
            NewPassword = new SecureString();
            ConfirmPassword = new SecureString();

            // Go into edited mode
            Editing ^= true;
        }

        #endregion

    }
}
