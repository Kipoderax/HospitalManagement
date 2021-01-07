using System.Threading.Tasks;
using System.Windows.Input;
using Dna;

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
        public string MyIdentify { get; set; }

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
            LoginCommand = new RelayParametrizedCommand( async parameter => await LoginAsync( parameter ) );
        }

        #endregion

        /// <summary>
        /// Employee authentication
        /// </summary>
        /// <param name="parameter">The employee password</param>
        /// <returns></returns>
        public async Task LoginAsync(object parameter)
        {
            await RunCommandAsync( () => LoginIsRunning, async () =>
             {
                 // Call the server and attempt to login with credentials
                 // TODO: Move all URLs and API routes to static class in core
                 var result = await WebRequests.PostAsync<ApiResponse<LoginResultApiModel>>(
                     "http://localhost:5000/api/auth/login",
                     new LoginEmployeeDto
                     {
                         Identify = MyIdentify,
                         Password = (parameter as IHavePassword)?.SecurePassword.UnSecure()
                     } );

                 // If there was no response, bad data or a response with a error message
                 if (result.DisplayErrorIfFailedAsync( "Login failed" ))
                     return;

                 // Ok successfully logged in.. now get employee data
                 var employeeData = result.ServerResponse.Response;

                 IoC.Settings.Token = result.ServerResponse.Response.Token;
                 IoC.Settings.FirstName = new TextEntryViewModel { Label = "Imię", OriginalText = employeeData?.FirstName };
                 IoC.Settings.LastName = new TextEntryViewModel { Label = "Nazwisko", OriginalText = employeeData?.LastName };
                 IoC.Settings.Identify = new TextEntryViewModel { Label = "Identyfikator", OriginalText = employeeData?.Username };
                 IoC.Settings.Type = new TextEntryViewModel { Label = "Posada", OriginalText = employeeData?.Type };
                 IoC.Settings.Specialize = new TextEntryViewModel { Label = "Specjalizacja", OriginalText = employeeData?.Specialize };
                 IoC.Settings.PwdNumber = new TextEntryViewModel { Label = "Numer PWD", OriginalText = employeeData?.NumberPwz };
                 IoC.Settings.Password = new PasswordEntryViewModel { Label = "Hasło", FakePassword = "********" };

                 // and get employee data
                 await IoC.Employees.LoadEmployees();
                 await IoC.Duties.LoadDuties();

                 await Task.Delay( 2000 );

                 // Go to work page
                 IoC.Get<ApplicationViewModel>().GoToPage( ApplicationPage.Work );
             } );
        }
    }
}
