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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _config;
        private readonly DataContext _context;

        #endregion

        #region DI Constructor

        /// <summary>
        /// Constructor with dependency injections
        /// </summary>
        /// <param name="authRepository"></param>
        /// <param name="employeeRepository"></param>
        /// <param name="config"></param>
        /// <param name="context"></param>
        public AuthController (IAuthRepository authRepository, 
                                                  IEmployeeRepository employeeRepository,
                                                  IConfiguration config,
                                                  DataContext context)
        {
            _authRepository = authRepository;
            _employeeRepository = employeeRepository;
            _config = config;
            _context = context;
        }

        #endregion

        /// <summary>
        /// Register new employee on the server
        /// </summary>
        /// <param name="employeeDto">Dto entity with register prop</param>
        /// <returns></returns>
        [HttpPost( "register" )]
        public async Task<ApiResponse<RegisterResultApiModel>> Register([FromBody] RegisterEmployeeDto employeeDto )
        {
            #region Empty Validates

            if (string.IsNullOrWhiteSpace(employeeDto.FirstName))
                return new ApiResponse<RegisterResultApiModel>
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pole imię pracownika jest puste"
                };

            if (string.IsNullOrWhiteSpace( employeeDto.LastName ))
                return new ApiResponse<RegisterResultApiModel>
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pole nazwisko pracownika jest puste"
                };

            if (string.IsNullOrWhiteSpace( employeeDto.Pesel ))
                return new ApiResponse<RegisterResultApiModel>
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pole pesel pracownika jest puste"
                };

            if (string.IsNullOrWhiteSpace( employeeDto.Type ))
                return new ApiResponse<RegisterResultApiModel>
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pole posada pracownika jest puste"
                };

            if (string.IsNullOrWhiteSpace( employeeDto.Specialize ))
                return new ApiResponse<RegisterResultApiModel>
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pole specjalizacja pracownika jest puste"
                };

            if (string.IsNullOrWhiteSpace( employeeDto.NumberPwz ))
                return new ApiResponse<RegisterResultApiModel>
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pole numer pwz pracownika jest puste"
                };

            #endregion

            string username = employeeDto?.FirstName.Substring( 0, 1 ) + employeeDto.LastName.Substring( 0, 1 ) + employeeDto?.Pesel[6..];
            // Make sure that employee with this username not exist
            if (await _authRepository.EmployeeExists( username ))
                return new ApiResponse<RegisterResultApiModel>()
                {
                    //TODO: Localize strings
                    ErrorMessage = "Pracownik z takim numerem pesel już istnieje"
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
        public async Task<ApiResponse<LoginResultApiModel>> LoginAsync( [FromBody] LoginEmployeeDto loginDto )
        {
            // Get employee from repo
            var employee = await _authRepository.Login( loginDto?.Identify, loginDto?.Password );

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

        [HttpPost("update")]
        public async Task<ApiResponse<UpdateEmployeeDto>> UpdateAsync(UpdateEmployeeDto updateDto)
        {
            #region Get Employee

            var employee = await _employeeRepository.GetEmployeeByUsername( updateDto.Username );

            if (employee == null)
                return new ApiResponse<UpdateEmployeeDto>
                {
                    ErrorMessage = "Nie znaleziono użytkownika"
                };

            #endregion
    
            #region Update Employee

            if (updateDto.FirstName != employee.FirstName)
            {
                employee.FirstName = updateDto.FirstName;
                employee.Username = employee.Username.Replace( 
                    employee.Username.Substring( 0, 1 ), 
                    updateDto.FirstName.Substring( 0, 1 ) );
            }

            if (updateDto.LastName != employee.LastName)
            {
                employee.LastName = updateDto.LastName;
                employee.Username = employee.Username.Replace( 
                    employee.Username.Substring( 1, 1 ), 
                    updateDto.LastName.Substring( 0, 1 ) );
            }

            if (updateDto.Type != null)
                employee.EmployeeType.EmployeeRole = updateDto.Type;

            if (updateDto.Specialize != null)
                employee.EmployeeSpecialize.SpecializeEmployee = updateDto.Specialize;

            if (updateDto.PwzNumber != null)
                employee.EmployeeSpecialize.NumberPwz = updateDto.PwzNumber;

            #endregion

            #region Value Validation

            if (!EmployeeValidate.PeselValidate( employee.Pesel ))
                return new ApiResponse<UpdateEmployeeDto>()
                {
                    ErrorMessage = "Pesel pracownika jest nie poprawny."
                };

            if (!EmployeeValidate.NumberPwzValidate( employee.EmployeeSpecialize.NumberPwz ))
                return new ApiResponse<UpdateEmployeeDto>()
                {
                    ErrorMessage = "Wpisano nie prawidłowo numer pwz pracownika"
                };

            #endregion

            #region Save Changes

            _employeeRepository.Update( employee );
            var result = await _context.SaveChangesAsync();
           
            #endregion
            
            #region Respond

            if( result > 0 )
                return new ApiResponse<UpdateEmployeeDto>
                {
                    Response = new UpdateEmployeeDto
                    {
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Username = employee.Username,
                        Type =  employee.EmployeeType.EmployeeRole,
                        Specialize = employee.EmployeeSpecialize.SpecializeEmployee,
                        PwzNumber = employee.EmployeeSpecialize.NumberPwz
                    }
                };
            return new ApiResponse<UpdateEmployeeDto>
            {
                ErrorMessage = "Update failed"
            };

            #endregion
        }
    }
}
