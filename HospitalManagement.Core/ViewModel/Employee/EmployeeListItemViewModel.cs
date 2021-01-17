using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Input;
using Dna;

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
        
        /// <summary>
        /// The command to load settings of selected employee
        /// </summary>
        public ICommand OpenEmployeeSettingsCommand { get; set; }

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

            OpenEmployeeSettingsCommand = new RelayParametrizedCommand(
                    async selected => await SettingsSelectedEmployeeAsync ( selected )
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

        /// <summary>
        /// Load settings of the selected employee
        /// </summary>
        /// <param name="selected">The selected username employee</param>
        /// <returns></returns>
        private async Task SettingsSelectedEmployeeAsync( object selected )
        {
            // Not allowed for other employees than administrator
            if( IoC.Settings.Type.OriginalText != "Administrator" ) return;
            
            
            // Get selected employee data
            var result = await WebRequests.PostAsync<ApiResponse<LoginResultApiModel>> (
                // TODO: Localize URL
                $"http://localhost:5000/api/employee/{selected}",
                bearerToken: IoC.Settings.Token
            );
            

            var dataEmployee = result.ServerResponse.Response;
   
            // If all right then
            if( result.Successful )
            {
                // load all need properties
                // TODO: Load duties of this employee
                IoC.Settings.FirstName.OriginalText = dataEmployee.FirstName;
                IoC.Settings.LastName.OriginalText = dataEmployee.LastName;
                IoC.Settings.Identify.OriginalText = dataEmployee.Username;
                IoC.Settings.Type.OriginalText = dataEmployee.Type;
                IoC.Settings.Specialize.OriginalText = dataEmployee.Specialize;
                IoC.Settings.PwdNumber.OriginalText = dataEmployee.NumberPwz;
                
                HideButtonsInOtherProfile();
                IoC.Application.SettingsMenuVisible = true;
            }
        }

        private void HideButtonsInOtherProfile()
        {
            IoC.Settings.IsEmployeeAdm = false;
            IoC.Settings.IsOtherProfile = true;
        }
    }
}
