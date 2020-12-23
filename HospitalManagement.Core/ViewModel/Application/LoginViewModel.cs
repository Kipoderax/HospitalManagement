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
                 // TODO: Fake a login...

                 // Ok successfully logged in.. now get users data
                 //TODO: Ask server for users info

                 // TODO: Remove this with real information pulled from our database in future
                 IoC.Settings.FirstName = new TextEntryViewModel { Label = "Imię", OriginalText = "Jessica" };
                 IoC.Settings.LastName = new TextEntryViewModel { Label = "Nazwisko", OriginalText = "Stalon" };
                 IoC.Settings.Identify = new TextEntryViewModel { Label = "Identyfikator", OriginalText = "JS12321" };
                 IoC.Settings.Type = new TextEntryViewModel { Label = "Posada", OriginalText = "Lekarz" };
                 IoC.Settings.Specialize = new TextEntryViewModel { Label = "Specjalizacja", OriginalText = "Urolog" };
                 IoC.Settings.PwdNumber = new TextEntryViewModel { Label = "Numer PWD", OriginalText = "pwd135" };
                 IoC.Settings.Password = new PasswordEntryViewModel { Label = "Hasło", FakePassword = "********" };

                 await Task.Delay( 2000 );

                 // Go to work page
                 IoC.Get<ApplicationViewModel>().GoToPage( ApplicationPage.Work );
             } );
        }
    }
}
