using System.Collections.Generic;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for a duty list
    /// </summary>
    public class DutyListViewModel : BaseViewModel
    {
        /// <summary>
        /// The duty list items for the list
        /// </summary>
        public List<DutyListItemViewModel> Items { get; set; }
    }
}
