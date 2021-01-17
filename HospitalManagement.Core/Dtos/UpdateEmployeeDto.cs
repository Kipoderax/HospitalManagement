namespace HospitalManagement.Core
{
    /// <summary>
    /// Employee details to change
    /// </summary>
    public class UpdateEmployeeDto
    {
        #region Public Properties

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        
        public string Pesel { get; set; }
        public string Type { get; set; }
        public string Specialize { get; set; }
        public string PwzNumber { get; set; }

        #endregion
    }
}
