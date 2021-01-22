using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Dna;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for overview employee list
    /// </summary>
    public class EmployeeListViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The employee list items for the list
        /// </summary>
        private ObservableCollection<EmployeeListItemViewModel> _items;

        /// <summary>
        /// The employee property to filter with this property
        /// </summary>
        private string _inputText = string.Empty;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// The employee list items for the list
        /// </summary>
        public ObservableCollection<EmployeeListItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged ( nameof(Items) );
            }
        }

        /// <summary>
        /// The employee property to filter with this property
        /// </summary>
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                OnPropertyChanged ( InputText );
            }
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to search employee
        /// </summary>
        public ICommand SearchEmployeeCommand { get; set; }

        #endregion

        #region Constructor

        public EmployeeListViewModel()
        {
            Items = new ObservableCollection<EmployeeListItemViewModel>();

            SearchEmployeeCommand = new RelayParametrizedCommand ( async text => await SearchEmployee ( text ) );
        }
        
        #endregion

        #region Command Methods

        /// <summary>
        /// Search employee by text represented properties of searching employee
        /// </summary>
        /// <param name="text">The property of searching employee</param>
        /// <returns></returns>
        public async Task SearchEmployee( object text )
        {
            // Load again employee for access searching employee
            await LoadEmployees();
            
            // Remove all duties which don't mismatch input text
            foreach ( var item in IoC.Employees.Items.ToList()
                
                .Where ( item => !item.Name.ToLower().Contains ( (string)text )
                                 && !item.Job.ToLower().Contains ( (string)text )
                                 && !item.Who.ToLower().Contains ( (string)text ) ) )
                
                IoC.Employees.Items.Remove ( item );

            // If employee nothing write then load all duties
            if( text.ToString().IsNullOWhiteSpace() )
                await LoadEmployees();
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Loads employees to application list for overview by other employees
        /// </summary>
        /// <returns></returns>
        public async Task LoadEmployees()
        {
            // Reload list for avoid override exists
            if (Items.Count > 0)
                Items.Clear();
            
            // Get result as the list of the employees for loading to app list
            var result = await WebRequests.PostAsync<ApiResponse<IEnumerable<EmployeeResultApiModel>>> (
                "http://localhost:5000/api/employee/employees",
                bearerToken: IoC.Settings.Token
            );
            
            // For every employee
            foreach ( var p in result.ServerResponse.Response )
            {
                // Decorate profile with border and picture
                MakeEmployeeInitial ( p, jobPicture: out var jobPicture, profileRGB: out var profileRGB);

                // Depend on type of the employee adding to list correct employee
                if (IoC.Settings.Type.OriginalText != "Administrator" && p.EmployeeType.EmployeeRole == "Administrator")
                    continue;
                
                // Fill list of the employees
                Items.Add ( new EmployeeListItemViewModel
                {
                    Name = p.FirstName + " " + p.LastName, 
                    Who = p.EmployeeType?.EmployeeRole,
                    Job = p.EmployeeSpecialize?.SpecializeEmployee,
                    JobPicture = jobPicture,
                    ProfilePictureRGB = profileRGB
                } );
            }
        }

        /// <summary>
        /// Retrieve login employee data by pesel
        /// TODO: Change property to Token for retrieve data
        /// </summary>
        /// <param name="pesel"></param>
        /// <returns></returns>
        public async Task LoadEmployeeByPesel( string pesel )
        {
            var result = await WebRequests.PostAsync<ApiResponse<LoginResultApiModel>> (
                // TODO: Localize URL
                $"http://localhost:5000/api/employee/retrieve/{pesel}",
                bearerToken: IoC.Settings.Token
            );

            var retrieveDataEmployee = result.ServerResponse.Response;
            
            // If all right then
            if( result.Successful )
            {
                // retrive back login employee data
                IoC.Settings.FirstName.OriginalText = retrieveDataEmployee.FirstName;
                IoC.Settings.LastName.OriginalText = retrieveDataEmployee.LastName;
                IoC.Settings.Identify.OriginalText = retrieveDataEmployee.Username;
                IoC.Settings.Type.OriginalText = retrieveDataEmployee.Type;
                IoC.Settings.Specialize.OriginalText = retrieveDataEmployee.Specialize;
                IoC.Settings.PwdNumber.OriginalText = retrieveDataEmployee.NumberPwz;
                
            }
        }
        
        /// <summary>
        /// Adding specify employee
        /// </summary>
        /// <param name="employee">The employee to add</param>
        public void AddNewEmployee(EmployeeListItemViewModel employee)
        {
            Items.Add ( employee );
        }
        
        #endregion

        #region Private Methods
        
        /// <summary>
        /// Decorate initial profile in the employee list with picture and border color
        /// depend on type
        /// </summary>
        /// <param name="employeeResult">The employee decorate for</param>
        /// <param name="profileRGB">The border color like #FF00FF</param>
        /// <param name="jobPicture">The picture inside border</param>
        private static void MakeEmployeeInitial(EmployeeResultApiModel employeeResult, out string profileRGB, out string jobPicture)
        {
            // Default initials
            profileRGB = string.Empty;
            jobPicture = string.Empty;
            
            // Switch role of the employee
            switch ( employeeResult.EmployeeType.EmployeeRole )
            {
                case "Lekarz":
                    profileRGB = "0BF90B"; jobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.png";
                    break;
                case "Pielęgniarka":
                    profileRGB = "0BD1F9"; jobPicture = @"pack://application:,,,/Images/EmployeeTypes/pielegniarka.jpg";
                    break;
                case "Administrator":
                    profileRGB = "E1F90B"; jobPicture = @"pack://application:,,,/Images/EmployeeTypes/Administrator.png";
                    break;
            }
        }
        
        #endregion
    }
}
