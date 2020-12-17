using System;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for each duty thread item in the duty thread
    /// </summary>
    public class DutyListItemViewModel : BaseViewModel
    {
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
    }
}
