namespace HospitalManagement.Core
{
    /// <summary>
    /// The result of a successful login request via API
    /// </summary>
    public class LoginResultApiModel
    {
        #region Public Properties

        /// <summary>
        /// The authentication token used to stay authenticated through future request
        /// </summary>
        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Type { get; set; }
        public string Specialize { get; set; }
        public string NumberPwz { get; set; }

        /// <summary>
        /// The employee username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The employee id
        /// </summary>
        public string Id { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public LoginResultApiModel ()
        {

        }

        #endregion
    }
}
