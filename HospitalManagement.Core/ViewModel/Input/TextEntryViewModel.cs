using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The view model for a text entry to edit a string value
    /// </summary>
    public class TextEntryViewModel : SettingsViewModel
    {
        #region Public Properties

        /// <summary>
        /// The label to identify what this value is for
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// The current saved value
        /// </summary>
        public string OriginalText { get; set; }

        /// <summary>
        /// The current non-commit edited text
        /// </summary>
        public string EditedText { get; set; }

        /// <summary>
        /// Indicates if the current text is in edit mode
        /// </summary>
        public bool Editing { get; set; }

        /// <summary>
        /// Indicates if the current control is pending an update
        /// </summary>
        public bool Updating { get; set; }

        /// <summary>
        /// The action to run when saving information
        /// Returns true if the commis was successful or false otherwise
        /// </summary>
        public Func<Task<bool>> CommitAction { get; set; }

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
        public TextEntryViewModel ()
        {
            // Create commands
            EditCommand = new RelayCommand( Edit );
            CancelCommand = new RelayCommand( Cancel );
            SaveCommand = new RelayCommand( Save );
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

            // Save currently saved value
            var currentSavedValue = OriginalText;

            RunCommandAsync( () => Updating, async () =>
             {
                 // While updating, come out of edit mode
                 Editing = false;

                 // Get new value to update
                 OriginalText = EditedText;
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
                      // Restore original value
                      EditedText = currentSavedValue;
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
            // Set the edited text to the current value
            EditedText = OriginalText;

            // Go into edited mode
            Editing ^= true;
        }

        #endregion

    }
}
