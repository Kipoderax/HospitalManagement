﻿using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalManagement.Core;
using HospitalManagement.Relational;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Server
{
    [Route("api/[controller]")]
    [AuthorizeToken]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Private Members

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        #endregion

        [Route("employees")]
        public async Task<ApiResponse<IEnumerable<EmployeeResultApiModel>>> GetAllEmployees()
        {
            var employee = await _employeeRepository.GetEmployeesAsync();

            var employeeApi = _mapper.Map<IEnumerable<EmployeeResultApiModel>> ( employee );
            
            return new ApiResponse<IEnumerable<EmployeeResultApiModel>>
            {
                Response = employeeApi
            };
        }
        
        [Route("retrieve/{pesel}")]
        public async Task<ApiResponse<LoginResultApiModel>> GetEmployeeByPesel( string pesel )
        {
            // Get employee from repo
            var employee = await _employeeRepository.GetEmployeeByPeselAsync ( pesel );

            // Make sure employee is not null
            if (employee == null)
                return new ApiResponse<LoginResultApiModel>
                {
                    ErrorMessage = "To tak nie moze byc"
                };
            

            return new ApiResponse<LoginResultApiModel> 
            {
                //TODO: Add employes duties to result response
                Response = new LoginResultApiModel
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Username = employee.Username,
                    Pesel = employee.Pesel,
                    Type = employee.EmployeeType.EmployeeRole,
                    Specialize = employee.EmployeeSpecialize.SpecializeEmployee,
                    NumberPwz = employee.EmployeeSpecialize.NumberPwz
                    
                }
            };
        }
        
        [Route("{username}")]
        public async Task<ApiResponse<LoginResultApiModel>> GetEmployeeByUsername( string username )
        {
            // Get employee from repo
            var employee = await _employeeRepository.GetEmployeeByNameAndLastNameAsync ( username );

            // Make sure employee is not null
            if (employee == null)
                return new ApiResponse<LoginResultApiModel>
                {
                    ErrorMessage = "To tak nie moze byc"
                };
            

            return new ApiResponse<LoginResultApiModel> 
            {
                Response = new LoginResultApiModel
                {
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Username = employee.Username,
                    Pesel = employee.Pesel,
                    Type = employee.EmployeeType.EmployeeRole,
                    Specialize = employee.EmployeeSpecialize.SpecializeEmployee,
                    NumberPwz = employee.EmployeeSpecialize.NumberPwz
                }
            };
        }
    }
}