using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalManagement.Core;

namespace HospitalManagement.Relational
{
    /// <summary>
    /// Management employees duties
    /// </summary>
    public interface IDutyRepository : IGenericRepository
    {
        /// <summary>
        /// Get list duties of the specify employee
        /// </summary>
        /// <param name="username">The employee username</param>
        /// <returns></returns>
        Task<IEnumerable<Duty>> GetEmployeeDutiesByUsername( string username );
    }
}