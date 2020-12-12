using System.Collections.Generic;

namespace HospitalManagement
{
    /// <summary>
    /// A view model for overview employee list
    /// </summary>
    public class EmployeeListViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The employee list items for the list
        /// </summary>
        public List<EmployeeListItemViewModel> Items { get; set; }

        #endregion
    }
}
