using System;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for each duty thread item in the duty thread
    /// </summary>
    public class DutyListItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Employee name on duty
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee first letter of last name with dot on duty
        /// </summary>
        public string FirstLetterOfLastName { get; set; }

        /// <summary>
        /// Start shift with seperate date from time on duty
        /// </summary>
        public DateTimeOffset StartShift { get; set; }

        /// <summary>
        /// End shift with seperate date from time on duty
        /// </summary>
        public DateTimeOffset EndShift { get; set; }

        /// <summary>
        /// Employee job name
        /// </summary>
        public string JobName { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to edit employee duty
        /// </summary>
        public ICommand EditDutyCommand { get; set; }

        #endregion

        #region Constructor

        public DutyListItemViewModel()
        {
            EditDutyCommand = new RelayParametrizedCommand ( date => EditEmployeeDuty(date) );
        }

        #endregion

        #region Command Methods
        
        /// <summary>
        /// Open edit mode for editing selected employee duty
        /// </summary>
        /// <param name="selectedDate">The duty with selected date to change</param>
        public void EditEmployeeDuty(object selectedDate)
        {
            if( IoC.Settings.IsOtherProfile == false && IoC.Settings.IsEmployeeAdm == false ) return;

            IoC.Settings.AttachmentMenuVisible ^= true;

            IoC.Settings.IsEditMode = true;
            IoC.Settings.SelectedDate = selectedDate.ToString();
        }
        
        #endregion
    }
}
