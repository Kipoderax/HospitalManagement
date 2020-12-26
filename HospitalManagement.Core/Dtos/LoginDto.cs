namespace HospitalManagement.Core
{
    /// <summary>
    /// Simply object of our employee entity for use in view application
    /// </summary>
    public class LoginDto
    {
        #region Public Properties

        /// <summary>
        /// Employee identify to log in
        /// </summary>
        public string Identify { get; set; }

        /// <summary>
        /// Employee password as pesel if is the first log in or not updated by employee.
        /// </summary>
        public string Password { get; set; }

        #endregion

        public LoginDto ()
        {

        }
    }
}
