using Dna;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

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

        /// <summary>
        /// Something wrong with register new employee
        /// </summary>
        public string ErrorMessage { get; set; }

        #region Employee Hints Information To Register

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public List<string> Types { get; set; }
        public List<string> Specializes { get; set; }
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
            FirstName = "Imię pracownika";
            LastName = "Nazwisko pracownika";
            Pesel = "Pesel pracownika";
            Types = new List<string> { "Administrator", "Lekarz", "Pielęgniarka" };
            Specializes = new List<string> { "Urolog", "Neurolog", "Kardiolog", "Laryngolog" };
            PwzNumber = "Numer prawa wykonywanego zawodu";

            RegisterCommand = new RelayCommand ( async () => await RegisterAsync() );
        }

        #endregion

        public async Task RegisterAsync()
        {
            await RunCommandAsync( () => RegisterIsRunning, async () =>
            {
                // Call the server and attempt to register
                // TODO: Move all URLs and API routes to static class in core
                var result = await WebRequests.PostAsync<ApiResponse<RegisterResultApiModel>>(
                    "http://localhost:5000/api/auth/register",
                    new RegisterDto
                    {
                        FirstName = FirstName,
                        LastName = LastName,
                        Pesel = Pesel,
                        Type = Types[0],
                        Specialize = Specializes[0],
                        NumberPwz = PwzNumber
                    } );

                if (result.DisplayErrorIfFailedAsync( "Register failed" ))
                    return;
                
                await Task.Delay( 2000 );

                // Go to work page
                IoC.Get<ApplicationViewModel>().GoToPage( ApplicationPage.Work );
            } );
        }
    }
}
