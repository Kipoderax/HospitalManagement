using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for each employee list item in the overview employee list
    /// </summary>
    public class EmployeeListItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The display name of this employee list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of employee like doctor, admin, ...
        /// </summary>
        public string Who { get; set; }

        /// <summary>
        /// The employee job
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// Picture present the employee job in the profile background
        /// </summary>
        public string JobPicture { get; set; }

        /// <summary>
        /// The RGB values (in hex) for the background color of the profile picture
        /// For example FF00FF for red and blue mixed
        /// </summary>
        public string ProfilePictureRGB { get; set; }

        /// <summary>
        /// True if user application clicked on the specific employee in search content
        /// </summary>
        public bool IsSelected { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to load duties of selected employee
        /// </summary>
        public ICommand OpenEmployeeDutiesCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public EmployeeListItemViewModel()
        {
            OpenEmployeeDutiesCommand = new RelayParametrizedCommand ( 
                    async selected => await DutiesSelectedEmployeeAsync(selected) 
                );
        }

        #endregion

        /// <summary>
        /// Load duties of the selected employee
        /// </summary>
        /// <param name="selected">The selected username employee</param>
        /// <returns></returns>
        private async Task DutiesSelectedEmployeeAsync(object selected)
        {
            await IoC.Duties.LoadDutiesBySelectedEmployee ( (string)selected );
        }
    }
}
