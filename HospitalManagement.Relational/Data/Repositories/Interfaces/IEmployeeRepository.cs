using HospitalManagement.Core;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagement.Relational
{
    /// <summary>
    /// Management employees like show list, get specify employee and so on
    /// </summary>
    public interface IEmployeeRepository : IGenericRepository
    {
        /// <summary>
        /// Get list of all employees
        /// </summary>
        /// <returns>All employees</returns>
        Task<IEnumerable<Employee>> GetEmployees ();

        /// <summary>
        /// Get list of all employees which aren't administrator type
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetNoAdmEmployees ();

        /// <summary>
        /// Get specify employee
        /// </summary>
        /// <param name="username">Primary key of the employee</param>
        /// <returns></returns>
        Task<Employee> GetEmployee ( string username );
    }
}
