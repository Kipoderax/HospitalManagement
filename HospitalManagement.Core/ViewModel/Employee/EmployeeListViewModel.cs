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
        
        public async Task LoadEmployees()
        {
            if (Items.Count > 0)
                Items.Clear();
            
            var result = await WebRequests.PostAsync<ApiResponse<IEnumerable<EmployeeResultApiModel>>> (
                "http://localhost:5000/api/employee/employees",
                bearerToken: IoC.Settings.Token
            );
            
            foreach ( var p in result.ServerResponse.Response )
            {
                Items.Add ( new EmployeeListItemViewModel
                {
                    Name = p.FirstName + " " + p.LastName, 
                    Who = p.EmployeeType?.EmployeeRole,
                    Job = p.EmployeeSpecialize?.SpecializeEmployee,
                    JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg",
                    ProfilePictureRGB = "3099c5"
                } );
            }
        }
        
        public void AddNewEmployee(EmployeeListItemViewModel employee)
        {
            Items.Add ( employee );
        }
    }
}
