using System.Security;

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

        #region Employee Personal Information

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Type { get; set; }
        public string Specialize { get; set; }
        public string NumberPwz { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        #endregion

        #endregion
    }
}
