using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Dna;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for a duty list
    /// </summary>
    public class DutyListViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The duty list items for the list
        /// </summary>
        private ObservableCollection<DutyListItemViewModel> _items;

        private ObservableCollection<DutyListItemViewModel> _employeeItems;

        private string _selectedSpecialize = "Wszyscy";

        #endregion

        #region Public Properties
        
        /// <summary>
        /// The duty list items for the list with real-time update after each change on the user scope
        /// </summary>
        public ObservableCollection<DutyListItemViewModel> Items
        {
            get => _items;
            set
            {
                // Update value
                _items = value;
                
                // Update to view with detect change
                OnPropertyChanged ( nameof(Items) );
            }
        }

        public ObservableCollection<DutyListItemViewModel> EmployeeItems
        {
            get => _employeeItems;
            set
            {
                // Update value
                _employeeItems = value;

                // Update to view with detect change
                OnPropertyChanged(nameof(EmployeeItems));
            }
        }

        public string SelectedSpecialize
        {
            get => _selectedSpecialize;
            set
            {
                _selectedSpecialize = value;

                OnPropertyChanged(_selectedSpecialize);
            }
        }
        
        #endregion
        
        #region Public Commands

        public ICommand SpecializeCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DutyListViewModel()
        {
            Items = new ObservableCollection<DutyListItemViewModel>();
            
            SpecializeCommand = new RelayParametrizedCommand(async specialize => await LoadDuties(specialize));
        }

        #endregion

        /// <summary>
        /// Load employee duties to employee settings view
        /// </summary>
        /// <param name="username">The employee username</param>
        /// <returns></returns>
        public async Task LoadEmployeeDuties(string username)
        {
            EmployeeItems ??= new ObservableCollection<DutyListItemViewModel>();
            
            // Clear before each request
            if (EmployeeItems.Count > 0)
                EmployeeItems.Clear();
            
            // Connect with server to get data
            var result = await WebRequests.PostAsync<ApiResponse<IEnumerable<DutyDto>>> (
                // TODO: Localize URL
                $"http://localhost:5000/api/duties/{username}",
                bearerToken: IoC.Settings.Token
            );
            
            // If all right then
            if (result.Successful)
                
                // put each taken item to list
                foreach ( var dutyResult in result.ServerResponse.Response )
                {
                    EmployeeItems.Add ( new DutyListItemViewModel
                    {
                        StartShift = dutyResult.StartShift,
                        EndShift = dutyResult.EndShift
                    } );
                }
        }

        /// <summary>
        /// Load duties of each employee to main list
        /// </summary>
        /// <returns></returns>
        public async Task LoadDuties(object specialize = null)
        {
            var spec = specialize as string;
            if (specialize == null) spec = "Wszyscy";

            // Make sure duty list not null
            Items ??= new ObservableCollection<DutyListItemViewModel>();

            // Clear before each request 
            if (Items.Count > 0)
                Items.Clear();
            
            // Connect with server to get data 
            var result = await WebRequests.PostAsync<ApiResponse<IEnumerable<DutyDto>>> (
                // TODO: Localize URL
                $"http://localhost:5000/api/duties/",
                bearerToken: IoC.Settings.Token
            );
            
            // If all right then
            if (result.Successful)
                foreach ( var dutyResult in result.ServerResponse.Response )
                {
                    if (spec != null && !spec.Equals("Wszyscy"))
                        if (!spec.Equals(dutyResult.Employee.EmployeeSpecialize.SpecializeEmployee))                                                                                continue;
                    if (IoC.Settings.Type.OriginalText != "Administrator" && dutyResult.Employee.EmployeeType.EmployeeRole == "Administrator") continue;

                    // put each taken item to list
                    Items.Add ( new DutyListItemViewModel
                    {
                        FirstName = dutyResult.Employee.FirstName + " " + dutyResult.Employee.LastName,
                        JobName = dutyResult.Employee.EmployeeSpecialize.SpecializeEmployee,
                        StartShift = dutyResult.StartShift,
                        EndShift = dutyResult.EndShift
                    } );
                }
        }
    }
}
