﻿using System;
using System.Security;
using System.Threading.Tasks;
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
        /// The current user password
        /// </summary>
        public SecureString UserPassword { get; set; }
        
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
        /// Indicates if the current control is pending an update
        /// </summary>
        public bool Updating { get; set; }

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
        
        /// <summary>
        /// The action to run when saving information
        /// Returns true if the commis was successful or false otherwise
        /// </summary>
        public Func<Task<bool>> CommitAction { get; set; }

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
            // Store the result of a commit call
            var result = default(bool);

            if( CurrentPassword.UnSecure() != UserPassword.UnSecure() )
            {
                ErrorMessage = "Hasło nie poprawne";
                return;
            }

            if( NewPassword.UnSecure() != ConfirmPassword.UnSecure() )
            {
                ErrorMessage = "Hasła do siebie nie pasują";
                return;
            }
            
            RunCommandAsync( () => Updating, async () =>
            {
                // While updating, come out of edit mode
                Editing = false;

                // Get new value to update
                CommitAction = IoC.Settings.UpdateEmployeeDetailAsync;
                
                // Try and do the work
                result = CommitAction == null || await CommitAction();
            } ).ContinueWith( t =>
            {
                // If we succeeded
                // Nothing to do
                // If we fail
                if (!result)
                {

                    // Go back into edit mode
                    Editing = true;
                }
            } );
        }

        /// <summary>
        /// Cancels out of edit mode
        /// </summary>
        private void Cancel ()
        {
            Editing = false;
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
