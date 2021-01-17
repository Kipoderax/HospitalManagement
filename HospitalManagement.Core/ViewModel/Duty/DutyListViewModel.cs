using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        /// <summary>
        /// The employee duty list items for the list with real-time update after each change on the user scope
        /// </summary>
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

        /// <summary>
        /// Specialize from combo box to overview employee list by selected 
        /// </summary>
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
            
            SpecializeCommand = new RelayParametrizedCommand(async specialize => await LoadDutiesByTypeAsync(specialize));
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Load duties of all employees by specialize
        /// </summary>
        /// <param name="specialize">The specialize of employee</param>
        /// <returns></returns>
        private async Task LoadDutiesByTypeAsync(object specialize = null)
        {
            // Load duties with the specialize
            await LoadDutiesAsync ();
            
            
            // If clicked Wszyscy do nothing
            if (specialize != null && specialize.Equals ( "Wszyscy" )) return;
            

            // For each employee duties which not contain selected specialize remove from list
            foreach ( var itemViewModel in IoC.Duties.Items.ToList()
                .Where ( itemViewModel => specialize != null 
                                          && !specialize.Equals ( itemViewModel.JobName ) ) )
            
                IoC.Duties.Items.Remove(itemViewModel);
            
            
            // Reload page of duty list with indicated specialize
            ReloadDutyList();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load employee duties to employee settings view
        /// </summary>
        /// <param name="username">The employee username</param>
        /// <returns></returns>
        public async Task LoadEmployeeDutiesAsync(string username)
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
        public async Task LoadDutiesAsync()
        {
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
                
                // For each duty
                foreach ( var dutyResult in result.ServerResponse.Response )
                {
                    
                    // Don't load duties administrator employee for employee which doesn't administrator
                    if (IoC.Settings.Type.OriginalText != "Administrator" 
                        && dutyResult.Employee.EmployeeType.EmployeeRole == "Administrator") continue;
                    

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
        
        /// <summary>
        /// Load duties of the selected employee
        /// </summary>
        /// <param name="username">The username selected employee</param>
        /// <returns></returns>
        public async Task LoadDutiesBySelectedEmployeeAsync( string username )
        {
            // Load duties with the specialize
            await LoadDutiesAsync ();

            // Show duties for only one selected employee by username
            foreach ( var itemViewModel in IoC.Duties.Items.ToList()
                .Where ( itemViewModel => username != null 
                                          && !username.Equals ( itemViewModel.FirstName ) ) )
            
                IoC.Duties.Items.Remove(itemViewModel);
            
            
            // Reload page of duty list with indicated specialize
            ReloadDutyList();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Reload duty list if changes noticed
        /// </summary>
        private void ReloadDutyList()
        {
            IoC.Application.GoToPage ( ApplicationPage.Work, new DutyListViewModel{Items = IoC.Duties.Items} );
        }

        #endregion
    }
}
