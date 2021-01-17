using HospitalManagement.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Relational
{
    /// <summary>
    /// Management employees like show list, get specify employee and so on
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
        public async Task<IEnumerable<Employee>> GetEmployeesAsync ()
        {
            var employees = await _dataContext.Employees.
                
                // Includes relation entities
                Include( d => d.EmployeeDuties ).
                Include ( t => t.EmployeeType ).
                Include ( s => s.EmployeeSpecialize ).
                
                // Collect results to list
                ToListAsync();

            return employees;
        }

        /// <summary>
        /// Get list of all employees which aren't administrator type
        /// NOTE: Should invoke while administrator of this application isn't login
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetNoAdmEmployeesAsync ()
        {
            // Get no adm employees to list
            // TODO: Localize string
            var noAdmEmployees = await _dataContext.Employees.
                Include(d => d.EmployeeDuties).
                Include ( t => t.EmployeeType ).
                Include ( s => s.EmployeeSpecialize ).
                Where( t => t.EmployeeType.EmployeeRole != "Administrator" ).
                ToListAsync();

            return noAdmEmployees;
        }

        /// <summary>
        /// Get specify employee
        /// </summary>
        /// <param name="username">Username of the employee</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByUsernameAsync ( string username )
        {
            // Employee with his duties
            var employee = await _dataContext.Employees
                .Include ( t => t.EmployeeType )
                .Include ( s => s.EmployeeSpecialize )
                .Include ( d => d.EmployeeDuties )
                .FirstOrDefaultAsync( e => e.Username == username );

            return employee;
        }
        
        /// <summary>
        /// Get specify employee
        /// </summary>
        /// <param name="username">Username of the employee</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByNameAndLastNameAsync ( string username )
        {
            var fullName = username.Split ( ' ' );
            
            // Employee with his duties
            var employee = await _dataContext.Employees
                .Include ( t => t.EmployeeType )
                .Include ( s => s.EmployeeSpecialize )
                .Include ( d => d.EmployeeDuties )
                .FirstOrDefaultAsync( e => e.FirstName == fullName[0] && e.LastName == fullName[1] );

            return employee;
        }

        /// <summary>
        /// Get employee by pesel
        /// TODO: Change pesel property to token
        /// </summary>
        /// <param name="pesel">The employee pesel</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployeeByPeselAsync( string pesel )
        {
            // Employee with his duties
            var employee = await _dataContext.Employees
                .Include ( t => t.EmployeeType )
                .Include ( s => s.EmployeeSpecialize )
                .FirstOrDefaultAsync( e => e.Pesel == pesel );

            return employee;
        }

        #endregion
    }
}
