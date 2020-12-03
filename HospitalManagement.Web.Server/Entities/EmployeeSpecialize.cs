using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// The specialize of the employee
    /// </summary>
    public class EmployeeSpecialize
    {
        #region Public Properties

        /// <summary>
        /// Auto-increment Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Foreign id of the employee
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The specialize employee
        /// </summary>
        public string SpecializeEmployee { get; set; }

        /// <summary>
        /// Number pwz of the employee
        /// </summary>
        public string NumberPwz { get; set; }

        public Employee Employee { get; set; }

        #endregion
    }
}
