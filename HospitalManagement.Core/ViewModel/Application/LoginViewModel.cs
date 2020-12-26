using Dna;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HospitalManagement.Core
{
    /// <summary>
    /// The View Model for a login screen
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The login of the user
        /// </summary>
        public string Identify { get; set; }

        /// <summary>
        /// A flag indicating if the login command is running
        /// </summary>
        public bool LoginIsRunning { get; set; }

        /// <summary>
        /// Something wrong with login to application
        /// </summary>
        public string ErrorMessage { get; set; }

        #endregion

        #region Commands

        public ICommand LoginCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginViewModel ()
        {
            LoginCommand = new RelayParametrizedCommand( async ( parameter ) => await LoginAsync( parameter ) );
        }

        #endregion

        public async Task LoginAsync(object parameter)
        {
            await RunCommand( () => LoginIsRunning, async () =>
             {
                 //// Call the server and attempt to login with credentials
                 //// TODO: Move all URLs and API routes to static class in core
                 //var result = await WebRequests.PostAsync<ApiResponse<LoginResultApiModel>>(
                 //    "http://localhost:5000/api/auth/login",
                 //    new LoginCredentialsApiModel
                 //    {
                 //        Username = Identify,
                 //        Password = (parameter as IHavePassword).SecurePassword.UnSecure()
                 //    } );

                 //// If there was no response, bad data or a response with a error message
                 //if (result == null || result.ServerResponse == null || !result.ServerResponse.Successful )
                 //{
                 //    // Default error message
                 //    // TODO: Localize strings
                 //    var message = "Unknown error from server call";

                 //    // If we got a response from the server
                 //    if (result?.ServerResponse != null)
                 //        // Set message to servers response
                 //        message = result.ServerResponse.ErrorMessage;

                 //    // If we have a result but deserialize failed
                 //    else if (string.IsNullOrWhiteSpace(result?.RawServerResponse))
                 //        // Set error message
                 //        message = $"Unexpected response from server. {result.RawServerResponse}";

                 //    // If we have a result but no server response details at all...
                 //    else if (result != null)
                 //        // Set message to standard HTTP server response details
                 //        message = $"Failed to communicate with server. Status code {result.StatusCode}. {result.StatusDescription}";

                 //    // Display error
                 //    //TODO: Localize string
                 //    ErrorMessage = $" Nie udane logowanie - {message}.";

                 //    // Enough failures
                 //    return;
                 //}

                 //// Ok successfully logged in.. now get users data
                 //var employeeData = result.ServerResponse.Response;

                 IoC.Settings.FirstName = new TextEntryViewModel { OriginalText = "Jessica" };
                 //IoC.Settings.FirstName = new TextEntryViewModel { Label = "Imię", OriginalText = employeeData.FirstName };
                 //IoC.Settings.LastName = new TextEntryViewModel { Label = "Nazwisko", OriginalText = employeeData.LastName };
                 //IoC.Settings.Identify = new TextEntryViewModel { Label = "Identyfikator", OriginalText = employeeData.Username };
                 //IoC.Settings.Type = new TextEntryViewModel { Label = "Posada", OriginalText = employeeData.Type };
                 //IoC.Settings.Specialize = new TextEntryViewModel { Label = "Specjalizacja", OriginalText = employeeData.Specialize };
                 //IoC.Settings.PwdNumber = new TextEntryViewModel { Label = "Numer PWD", OriginalText = employeeData.NumberPwz };
                 //IoC.Settings.Password = new PasswordEntryViewModel { Label = "Hasło", FakePassword = "********" };

                 await Task.Delay( 2000 );

                 // Go to work page
                 IoC.Get<ApplicationViewModel>().GoToPage( ApplicationPage.Work );
             } );
        }
    }
}
