using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HospitalManagement.Web.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Private Members

        private readonly IAuthRepository _authRepository;

        #endregion

        #region DI Constructor

        /// <summary>
        /// Constructor with dependency injections
        /// </summary>
        /// <param name="authRepository"></param>
        public AuthController (IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        #endregion

        /// <summary>
        /// Register new employee to database
        /// </summary>
        /// <param name="employeeDto">Dto employee object</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(EmployeeRegisterDto employeeDto)
        {
            // Make sure that employee with this username not exist
            if (await _authRepository.EmployeeExists( employeeDto.FirstName.Substring( 0, 1 ) + employeeDto.LastName.Substring( 0, 1 ) + employeeDto.Pesel[6..] ))
                return BadRequest( "Taki pracownik już został dodany" );

            // Create employee
            var employeeToCreate = new Employee
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Pesel = employeeDto.Pesel,
                Username = employeeDto.FirstName.Substring( 0, 1 ) + employeeDto.LastName.Substring( 0, 1 ) + employeeDto.Pesel[6..],
                IsFirstLogin = true,
                AccountCreated = DateTime.Now,
                EmployeeType = new EmployeeType
                {
                    EmployeeRole = employeeDto.Type
                },
                EmployeeSpecialize = new EmployeeSpecialize
                {
                    SpecializeEmployee = employeeDto.Specialize,
                    NumberPwz = employeeDto.NumberPwz
                }
            };

            var createdEmployee = await _authRepository.Register(employeeToCreate, employeeDto.Pesel);

            return StatusCode( 201 );
        }
    }
}
