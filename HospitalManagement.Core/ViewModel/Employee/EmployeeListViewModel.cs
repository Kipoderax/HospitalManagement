using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Dna;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for overview employee list
    /// </summary>
    public class EmployeeListViewModel : BaseViewModel
    {
        #region Protected Members

        /// <summary>
        /// The employee list items for the list
        /// </summary>
        private ObservableCollection<EmployeeListItemViewModel> _items;

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

        #endregion

        public EmployeeListViewModel()
        {
            Items = new ObservableCollection<EmployeeListItemViewModel>();
        }
        
        /// <summary>
        /// Loads employees to application list for overview by other employees
        /// </summary>
        /// <returns></returns>
        public async Task LoadEmployees()
        {
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
        /// Adding specify employee
        /// </summary>
        /// <param name="employee">The employee to add</param>
        public void AddNewEmployee(EmployeeListItemViewModel employee)
        {
            Items.Add ( employee );
        }

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
    }
}
