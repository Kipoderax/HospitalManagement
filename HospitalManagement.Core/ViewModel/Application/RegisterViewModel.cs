using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Dna;

namespace HospitalManagement.Core
{
    /// <summary>
    /// Register employee forms as view model
    /// </summary>
    public class RegisterViewModel : BaseViewModel
    {
        #region Singleton

        /// <summary>
        /// A single instance of the view model
        /// </summary>
        public static RegisterViewModel Instance => new RegisterViewModel();

        #endregion

        #region Public Properties

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool RegisterIsRunning { get; set; }


        #region Combo Box Items In Register Forms

        public List<string> Types { get; set; } = new List<string> { "Administrator", "Lekarz", "Pielęgniarka" };
        public List<string> Specializes { get; set; } = new List<string> { "Urolog", "Neurolog", "Kardiolog", "Laryngolog" };

        #endregion

        #region Employee Details From Register Forms

        /// <summary>
        /// Employee first name from register form
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee last name from register form
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Employee pesel from register form
        /// </summary>
        public string Pesel { get; set; }

        /// <summary>
        /// Employee type from register form
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Employee specjalize from register form
        /// </summary>
        public string Specialize { get; set; }

        /// <summary>
        /// Employee pwz number from register form
        /// </summary>
        public string PwzNumber { get; set; }

        #endregion

        #endregion

        #region Commands

        public ICommand RegisterCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegisterViewModel ()
        {
            // Set tags to register view form
            RegisterCommand = new RelayCommand ( async () => await RegisterAsync() );
        }

        #endregion

        #region Command Methods

        public async Task RegisterAsync()
        {
            await RunCommandAsync( () => RegisterIsRunning, async () =>
            {
                // Call the server and attempt to register
                // TODO: Move all URLs and API routes to static class in core
                var result = await WebRequests.PostAsync<ApiResponse<RegisterResultApiModel>>(
                    "http://localhost:5000/api/auth/register",
                    new RegisterEmployeeDto
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Pesel = Pesel,
                        Type = Type,
                        Specialize = Specialize,
                        NumberPwz = PwzNumber
                    } 
                );
                

                if (result.DisplayErrorIfFailedAsync())
                {
                    Success = false;
                    ErrorMessage = result.ErrorMessage;
                    return;
                }
                
                
                Success = true;
                var dataEmployee = result.ServerResponse.Response;
                SuccessMessage = $"Zarejestrowano pracownika - {dataEmployee.FirstName} {dataEmployee.LastName}";
                
                // Update employee list with this new
                IoC.Employees.AddNewEmployee ( new EmployeeListItemViewModel
                {
                    Name = dataEmployee.FirstName + " " + dataEmployee.LastName,
                    Who = dataEmployee.Type,
                    Job = dataEmployee.Specialize,
                    ProfilePictureRGB = "cceeff",
                    JobPicture = @"pack://application:,,,/Images/EmployeeTypes/Doctor.jpg"
                });
            } );
            
            await Task.Delay( 2000 );
            SuccessMessage = "";
            ErrorMessage = "";
        }

        #endregion
    }
}
