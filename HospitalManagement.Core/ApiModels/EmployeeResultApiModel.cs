namespace HospitalManagement.Core
{
    /// <summary>
    /// The result of get employee list to view of the start page after successful login
    /// </summary>
    public class EmployeeResultApiModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeSpecializeDto EmployeeSpecialize { get; set; }
        public EmployeeTypeDto EmployeeType { get; set; }
    }
}