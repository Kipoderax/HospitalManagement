﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalManagement.Core;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Relational
{
    /// <summary>
    /// Management employees duties
    /// </summary>
    public class DutyRepository : GenericRepository, IDutyRepository
    {
        #region Private Members

        /// <summary>
        /// The scope application data context
        /// </summary>
        private readonly DataContext _dataContext;

        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataContext">The injected context</param>
        /// <param name="employeeRepository">The injected employee repository</param>
        public DutyRepository(
                DataContext dataContext, 
                IEmployeeRepository employeeRepository) : base(dataContext)
        {
            _dataContext = dataContext;
            _employeeRepository = employeeRepository;
        }
        
        #endregion

        #region Implemented Methods

        /// <summary>
        /// Get list duties of the specify employee
        /// </summary>
        /// <param name="username">The employee username</param>
        /// <returns></returns>
        public async Task<IEnumerable<Duty>> FindEmployeeDutiesByUsernameAsync( string username )
        {
            var duty = await _dataContext.Duties
                    
                // filter by logged employee
                .Where( u => u.Employee.Username == username )
                .OrderByDescending ( d => d.StartShift )
                .ToListAsync();

            return duty;
        }

        /// <summary>
        /// Get duty by username and start shift
        /// </summary>
        /// <param name="dutyDto">The employee duty information to find</param>
        /// <returns></returns>
        public async Task<Duty> FindEmployeeDutyByStartShiftAndUsernameAsync( DutyDto dutyDto )
        {
            var duty = await _dataContext.Duties

                // filter by logged employee
                .Where ( u => u.Employee.Username == dutyDto.Employee.Username )
                .Where ( s => s.StartShift == dutyDto.Employee.EmployeeDuties.First().StartShift )
                .FirstAsync();

            return duty;
        }

        /// <summary>
        /// Get duties list of all employees
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Duty>> GetEmployeeDutiesAsync()
        {
            var duties = await _dataContext.Duties
                .Include ( e => e.Employee )
                .Include ( s => s.Employee.EmployeeSpecialize )
                .Include ( t => t.Employee.EmployeeType )
                .OrderByDescending ( d => d.StartShift )
                .ToListAsync();

            return duties;
        }

        /// <summary>
        /// Get duties of employees aren't administrator
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Duty>> GetNoAdmEmployeeDutiesAsync()
        {
            var duties = await _dataContext.Duties
                .Include ( e => e.Employee )
                .Include ( s => s.Employee.EmployeeSpecialize )
                .Include ( t => t.Employee.EmployeeType )
                .Where ( u => u.Employee.EmployeeType.EmployeeRole.Equals ( "Administrator" ) )
                .OrderByDescending ( d => d.StartShift )
                .ToListAsync();

            return duties;
        }

        /// <summary>
        /// Register employee duty
        /// </summary>
        /// <param name="dutyDto">The duty dto arrived from view form </param>
        /// <returns></returns>
        public async Task<bool> AddDutyAsync(DutyDto dutyDto)
        {
            var duty = new Duty();
            
            // Get id of the employee for connect with them duties   
            var employeeId = await _employeeRepository.GetEmployeeByUsernameAsync ( dutyDto.Employee.Username );

            // Assign to duty model
            duty.StartShift = dutyDto.StartShift;
            duty.EndShift = dutyDto.EndShift;
            duty.EmployeeId = employeeId.UserId;

            // If no conflict with other duties add to data context
            await _dataContext.AddAsync ( duty );
            
            // Save finally
            return await _dataContext.SaveChangesAsync() > 0;
        }

        #endregion
    }
}