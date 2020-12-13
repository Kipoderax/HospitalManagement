﻿using System.Threading.Tasks;
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
                 await Task.Delay( 2000 );
             } );
        }
    }
}
