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
        /// Register new employee to database
        /// </summary>
        /// <param name="employeeDto">Dto entity with register prop</param>
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

            if (!EmployeeValidate.PeselValidate( employeeDto.Pesel ))
                return BadRequest( "Pesel pracownika jest nie poprawny." );

            if (!EmployeeValidate.NumberPwzValidate( employeeDto.NumberPwz ))
                return BadRequest( "Wpisano nie prawidłowo numer pwz pracownika" );

            var createdEmployee = await _authRepository.Register(employeeToCreate, employeeDto.Pesel);

            return StatusCode( 201 );
        }

        /// <summary>
        /// Login employee to app via token authentication
        /// Unnecessary request to database
        /// </summary>
        /// <param name="loginDto">Dto entity with login prop</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login( EmployeeLoginDto loginDto )
        {
            // Get employee from repo
            var employee = await _authRepository.Login( loginDto.Identify, loginDto.Password );

            // Make sure employee is not null
            if (employee == null)
                return Unauthorized();

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

            return Ok( new { token = tokenHandler.WriteToken( token ) } );
        }
    }
}
