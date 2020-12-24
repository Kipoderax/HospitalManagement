namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Simply object of our employee entity for use in view application
    /// </summary>
    public class EmployeeRegisterDto
    {
        #region Public Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Type { get; set; }
        public string Specialize { get; set; }
        public string NumberPwz { get; set; }

        #endregion
    }
}
