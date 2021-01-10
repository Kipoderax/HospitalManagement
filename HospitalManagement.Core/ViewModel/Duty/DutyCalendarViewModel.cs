using Dna;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// A view model for the get duty date
    /// </summary>
    public class DutyCalendarViewModel : BaseViewModel
    {
        #region Private Members

        private DateTime _start;
        private DateTime _end;
        private string _hourTime;
        private string _minutTime;
        private DateTime _selectedDate;

        #endregion
        
        #region Public Properties

        /// <summary>
        /// The range from begin employees can't set self duty
        /// </summary>
        public DateTime Start 
        {
            get => _start;
            set
            {
                // Update value
                _start = value;
                
                // Update to view with detect change
                OnPropertyChanged ( nameof(Start) );
            }
        }

        /// <summary>
        /// The range to end employees can't set self duty
        /// </summary>
        public DateTime End 
        {
            get => _end;
            set
            {
                // Update value
                _end = value;
                
                // Update to view with detect change
                OnPropertyChanged ( nameof(End) );
            }
        }

        /// <summary>
        /// Part time of hour for duty set
        /// </summary>
        public string HourTime 
        {
            get => _hourTime;
            set
            {
                // Update value
                _hourTime = value;
                
                // Update to view with detect change
                OnPropertyChanged ( nameof(HourTime) );
            }
        }

        /// <summary>
        /// Part time of minut for duty set
        /// </summary>
        public string MinutTime 
        {
            get => _minutTime;
            set
            {
                // Update value
                _minutTime = value;
                
                // Update to view with detect change
                OnPropertyChanged ( nameof(MinutTime) );
            }
        }

        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                // Update value
                _selectedDate = value;
                
                // Update to view with detect change
                OnPropertyChanged ( nameof(SelectedDate) );
            }
        }

        public bool AddDutyIsRunning { get; set; }

        #endregion

        #region Public Commands

        /// <summary>
        /// The command to add duty
        /// </summary>
        public ICommand AddDutyCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public DutyCalendarViewModel ()
        {
            Start = DateTime.Now.AddDays( 30 );
            End = Start.AddMonths( 1 );

            AddDutyCommand = new RelayCommand ( async () => await AddDutyAsync() );
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Add duty by login employee or administrator
        /// NOTE: username null means that duty add by login employee
        ///       otherwise duty is adding by administrator for employee which have
        ///       this username
        /// </summary>
        /// <param name="username">The employee username</param>
        /// <returns></returns>
        public async Task AddDutyAsync(string username = null)
        {
            var employee = new Employee();
            await RunCommandAsync ( () => AddDutyIsRunning, async () =>
            { 
                
                // Check if username is not null
                employee.Username = username ?? IoC.Settings.Identify.OriginalText;
                
                
                // Check if no exist duty with specialize contain by employee add for
                if( ! await DutyValidate.IsUniqueSpecialize ( SelectedDate, IoC.Settings.Specialize.OriginalText ) )
                    return false;
                
                
                // Check if employee get date from correct date-range
                if( ! DutyValidate.IsDateRangeCorrect ( SelectedDate, End ) )
                    return false;
                
                
                // Check if employee don't get duty 1 day after end shift
                if( ! DutyValidate.IsDayAfterOrBefore ( SelectedDate ) )
                    return false;
                
                // Check if employee in selected month have more than 10 duties
                if( DutyValidate.AmountDutiesInMoth ( SelectedDate ) >= 10 )
                    return false;
                

                var result = await WebRequests.PostAsync<ApiResponse<DutyDto>> (
                    "http://localhost:5000/api/duties/add",
                    new DutyDto
                    {
                        StartShift = SelectedDate.Date.Add ( TimeSpan.Parse ( HourTime + ":" + MinutTime ) ),
                        EndShift = SelectedDate.AddDays ( 1 ).Date.Add ( TimeSpan.Parse ( HourTime + ":" + MinutTime ) ),
                        Employee = employee
                    },
                    bearerToken: IoC.Settings.Token
                );
                

                if( result.Successful )
                    await IoC.Duties.LoadEmployeeDuties ( IoC.Settings.Identify.OriginalText );
                

                return true;
            } );
        }

        #endregion
    }
}
