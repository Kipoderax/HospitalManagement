namespace HospitalManagement.Core
{
    /// <summary>
    /// The credentials for an API client to log into the server and receive a token back
    /// </summary>
    public class LoginCredentialsApiModel
    {
        #region Public Properties

        /// <summary>
        /// The employee username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The employee password
        /// </summary>
        public string Password { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginCredentialsApiModel ()
        {

        }

        #endregion
    }
}
