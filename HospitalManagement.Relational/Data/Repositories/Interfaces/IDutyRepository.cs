using System;
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
        Task<IEnumerable<Duty>> FindEmployeeDutiesByUsername( string username );

        /// <summary>
        /// Get duties list of all employees
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Duty>> GetEmployeeDuties();

        /// <summary>
        /// Get duties list of all employees without administrators
        /// if authentication employee isn't administrator
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Duty>> GetNoAdmEmployeeDuties();

        /// <summary>
        /// Register employee duty
        /// </summary>
        /// <param name="dutyDto">The duty dto arrived from view form </param>
        /// <returns></returns>
        Task<bool> AddDutyAsync( DutyDto dutyDto );
    }
}