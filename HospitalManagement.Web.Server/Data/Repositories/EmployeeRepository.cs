using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Management employees 
    /// </summary>
    public class EmployeeRepository : GenericRepository, IEmployeeRepository
    {
        #region Private Members

        /// <summary>
        /// data context based on database
        /// </summary>
        private readonly DataContext _dataContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataContext">Injected data context</param>
        public EmployeeRepository (DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        #endregion

        #region Implemented Methods

        /// <summary>
        /// Get list of all employees
        /// </summary>
        /// <returns>All employees</returns>
        public async Task<IEnumerable<Employee>> GetEmployees ()
        {
            var employees = await _dataContext.Employees.ToListAsync();

            return employees;
        }

        #endregion
    }
}
