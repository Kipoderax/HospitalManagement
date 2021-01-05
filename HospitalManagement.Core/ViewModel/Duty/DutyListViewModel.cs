using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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

        #endregion

        #region Public Properties
        
        /// <summary>
        /// The duty list items for the list
        /// </summary>
        public ObservableCollection<DutyListItemViewModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged ( nameof(Items) );
            }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DutyListViewModel()
        {
            Items = new ObservableCollection<DutyListItemViewModel>();
        }

        #endregion

        public async Task LoadEmployeeDuties(string username)
        {
            if (Items.Count > 0)
                Items.Clear();
            
            var result = await WebRequests.PostAsync<ApiResponse<IEnumerable<DutyResultApiModel>>> (
                // TODO: Localize URL
                $"http://localhost:5000/api/duties/{username}",
                bearerToken: IoC.Settings.Token
            );
            
            if (result.Successful)
                foreach ( var dutyResult in result.ServerResponse.Response )
                {
                    Items.Add ( new DutyListItemViewModel
                    {
                        StartShift = dutyResult.StartShift,
                        EndShift = dutyResult.EndShift
                    } );
                }
        }
    }
}
