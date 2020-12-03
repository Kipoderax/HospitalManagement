using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Hospital employee
    /// </summary>
    public class Employee
    {
        #region Public Properties

        /// <summary>
        /// Auto-increment id
        /// </summary>
        [Key]
        public int UserId { get; set; }

        /// <summary>
        /// Emploee name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Employee last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Employee pesel
        /// </summary>
        public string Pesel { get; set; }

        /// <summary>
        /// Unique username of the employee
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Encrypt hash password
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Encrypt salt password
        /// </summary>
        public byte[] PasswordSalt { get; set; }

        /// <summary>
        /// First login allow employee to set new password if true
        /// </summary>
        public bool IsFirstLogin { get; set; }

        /// <summary>
        /// Create-data employee to application
        /// </summary>
        public DateTime AccountCreated { get; set; }

        /// <summary>
        /// Role of the employee
        /// </summary>
        public EmployeeType EmployeeType { get; set; }

        /// <summary>
        /// Specialize of the employee
        /// </summary>
        public EmployeeSpecialize EmployeeSpecialize { get; set; }

        /// <summary>
        /// Set of all duties
        /// </summary>
        public ICollection<Duty> EmployeeDuties { get; set; }

        #endregion
    }
}
