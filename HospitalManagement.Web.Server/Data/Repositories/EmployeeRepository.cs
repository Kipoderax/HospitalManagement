using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Web.Server
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
        public async Task<IEnumerable<Employee>> GetEmployees ()
        {
            var employees = await _dataContext.Employees.
                Include( d => d.EmployeeDuties ).
                ToListAsync();

            return employees;
        }

        /// <summary>
        /// Get list of all employees which aren't administrator type
        /// NOTE: Should invoke while administrator of this application is login
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetNoAdmEmployees ()
        {
            // Get no adm employees to list
            // TODO: Localize string
            var noAdmEmployees = await _dataContext.Employees.
                Include(d => d.EmployeeDuties).
                Where( t => t.EmployeeType.EmployeeRole != "Administrator" ).
                ToListAsync();

            return noAdmEmployees;
        }

        /// <summary>
        /// Get specify employee
        /// </summary>
        /// <param name="id">Primary key of the employee</param>
        /// <returns></returns>
        public async Task<Employee> GetEmployee ( int id )
        {
            // Employee with his duties
            var employee = await _dataContext.Employees
                .Include( d => d.EmployeeDuties )
                .FirstOrDefaultAsync( e => e.UserId == id );

            return employee;
        }

        #endregion
    }
}
