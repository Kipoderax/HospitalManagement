using HospitalManagement.Core;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Relational
{
    /// <summary>
    /// The database representational model for our application
    /// </summary>
    public class DataContext : DbContext
    {
        #region Public Properties

        /// <summary>
        /// The settings for the application
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<EmployeeSpecialize> EmployeeSpecializes { get; set; }
        public DbSet<Duty> Duties { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }

        #endregion
    }
}
