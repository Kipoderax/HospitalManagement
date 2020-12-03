using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Duty of employees
    /// </summary>
    public class Duty
    {
        #region Public Properties

        /// <summary>
        /// Auto-increment id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Employee id
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Full date for start shift duty of the employee
        /// </summary>
        public DateTime StartShift { get; set; }

        /// <summary>
        /// Full date for end shift duty of the employee
        /// </summary>
        public DateTime EndShift { get; set; }

        public Employee Employee { get; set; }

        #endregion
    }
}
