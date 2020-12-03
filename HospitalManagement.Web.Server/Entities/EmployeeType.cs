using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Role of the employees
    /// </summary>
    public class EmployeeType
    {
        #region Public Properties

        /// <summary>
        /// Auto-increment id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign id of the employee
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The role of the employee
        /// </summary>
        public string EmployeeRole { get; set; }


        public Employee Employee { get; set; }

        #endregion
    }
}
