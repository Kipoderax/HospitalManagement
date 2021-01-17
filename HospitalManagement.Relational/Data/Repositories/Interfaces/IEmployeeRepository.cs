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
        Task<IEnumerable<Employee>> GetEmployeesAsync ();

        /// <summary>
        /// Get list of all employees which aren't administrator type
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Employee>> GetNoAdmEmployeesAsync ();

        /// <summary>
        /// Get specify employee
        /// </summary>
        /// <param name="username">Primary key of the employee</param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByUsernameAsync ( string username );

        /// <summary>
        /// Get employee by first and last name
        /// </summary>
        /// <param name="username">The first and last name of the employee</param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByNameAndLastNameAsync( string username );

        /// <summary>
        /// Get employee by pesel
        /// NOTE: Better way is get employee by token
        /// </summary>
        /// <param name="pesel">The employee pesel</param>
        /// <returns></returns>
        Task<Employee> GetEmployeeByPeselAsync( string pesel );
    }
}
