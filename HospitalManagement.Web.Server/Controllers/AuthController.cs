using HospitalManagement.Core;
using HospitalManagement.Relational;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Web.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Private Members

        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;

        #endregion

        #region DI Constructor

        /// <summary>
        /// Constructor with dependency injections
        /// </summary>
        /// <param name="authRepository"></param>
        public AuthController (IAuthRepository authRepository, IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }

        #endregion

        /// <summary>
        /// Register new employee on the server
        /// </summary>
        /// <param name="employeeDto">Dto entity with register prop</param>
        /// <returns></returns>
        [HttpPost( "register" )]
        public async Task<ApiResponse<RegisterResultApiModel>> Register([FromBody] RegisterDto employeeDto )
        {
            // Make sure that employee with this username not exist
            if (await _authRepository.EmployeeExists( employeeDto?.FirstName.Substring( 0, 1 ) + employeeDto?.LastName.Substring( 0, 1 ) + employeeDto?.Pesel[6..] ))
                return new ApiResponse<RegisterResultApiModel>()
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pracownik z takim numere pesel już istnieje"
                };

            // Create employee
            var employeeToCreate = new Employee
            {
                FirstName = employeeDto?.FirstName,
                LastName = employeeDto?.LastName,
                Pesel = employeeDto?.Pesel,
                Username = employeeDto.FirstName.Substring( 0, 1 ) + employeeDto.LastName.Substring( 0, 1 ) + employeeDto.Pesel[6..],
                IsFirstLogin = true,
                AccountCreated = DateTime.Now,
                EmployeeType = new EmployeeType
                {
                    EmployeeRole = employeeDto?.Type
                },
                EmployeeSpecialize = new EmployeeSpecialize
                {
                    SpecializeEmployee = employeeDto?.Specialize,
                    NumberPwz = employeeDto?.NumberPwz
                }
            };

            if (!EmployeeValidate.PeselValidate( employeeDto.Pesel ))
                return new ApiResponse<RegisterResultApiModel>()
                {
                    ErrorMessage = "Pesel pracownika jest nie poprawny."
                };

            if (!EmployeeValidate.NumberPwzValidate( employeeDto.NumberPwz ))
                return new ApiResponse<RegisterResultApiModel>()
                {
                    ErrorMessage = "Wpisano nie prawidłowo numer pwz pracownika"
                };

            // Saving new account to database
            var createdEmployee = await _authRepository.Register( employeeToCreate, employeeDto.Pesel );

            return new ApiResponse<RegisterResultApiModel>
            {
                Response = new RegisterResultApiModel
                {
                    FirstName = createdEmployee.FirstName,
                    LastName = createdEmployee.LastName,
                    Username = createdEmployee.Username,
                    Pesel = createdEmployee.Pesel,
                    Type = createdEmployee.EmployeeType.EmployeeRole,
                    Specialize = createdEmployee.EmployeeSpecialize.SpecializeEmployee,
                    NumberPwz = createdEmployee.EmployeeSpecialize.NumberPwz
                }
            };
        }

        /// <summary>
        /// Login employee to app via token authentication
        /// Unnecessary request to database
        /// </summary>
        /// <param name="loginDto">Dto entity with login prop</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ApiResponse<LoginResultApiModel>> Login( [FromBody] LoginDto loginDto )
        {
            // Get employee from repo
            var employee = await _authRepository.Login( loginDto.Identify, loginDto.Password );

            // Make sure employee is not null
            if (employee == null)
                return new ApiResponse<LoginResultApiModel>
                {
                    ErrorMessage = "Zły login lub hasło"
                };

            // Create token for id and username
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, employee.UserId.ToString()),
                new Claim(ClaimTypes.Name, employee.Username)
            };

            // Create key from token
            var tokenKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( _config.GetSection( "AppSettings:Token" ).Value ) );

            // Generate credentials of our token
            var tokenCredentials = new SigningCredentials( tokenKey, SecurityAlgorithms.HmacSha512Signature );

            // 
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( claims ),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = tokenCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken( tokenDescriptor );

            return new ApiResponse<LoginResultApiModel> 
            {
                //TODO: Add employes duties to result response
                Response = new LoginResultApiModel
                {
                    Token = tokenHandler.WriteToken(token),
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
