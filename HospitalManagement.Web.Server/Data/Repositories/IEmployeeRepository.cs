using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalManagement.Web.Server
{
    /// <summary>
    /// Management employees like show list, get specify employee and so on
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Get list of all employees
        /// </summary>
        /// <returns>All employees</returns>
        Task<IEnumerable<Employee>> GetEmployees ();
    }
}
