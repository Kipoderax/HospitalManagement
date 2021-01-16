using HospitalManagement.Core;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Relational
{
    /// <summary>
    /// Authorization employee for hospital application
    /// </summary>
    public class AuthRepository : IAuthRepository
    {
        #region Private Members

        /// <summary>
        /// context modify tables
        /// </summary>
        private readonly DataContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor with inject context
        /// </summary>
        /// <param name="context"></param>
        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        #endregion

        #region Implemented Methods

        /// <summary>
        /// Login employee to application
        /// </summary>
        /// <param name="username">Unique username</param>
        /// <param name="password">Employee password</param>
        /// <returns>login employee</returns>
        public async Task<Employee> Login(string username, string password)
        {
            // Find employee with username
            Employee employee = await _context.Employees
                .Include(t => t.EmployeeType)
                .Include(s => s.EmployeeSpecialize)
                .Include(d => d.EmployeeDuties)
                .FirstOrDefaultAsync(u => u.Username == username);

            // Make sure employee is not null
            if (employee == null)
                return null;

            // If validation is wrong, return
            if (!VerifyPasswordHash(password, employee.PasswordHash, employee.PasswordSalt))
                return null;

            return employee;
        }

        /// <summary>
        /// Register new employee to hospital application
        /// </summary>
        /// <param name="employee">Set data of employee to register</param>
        /// <param name="password">Password of the employee</param>
        /// <returns>new employee</returns>
        public async Task<Employee> Register(Employee employee, string password)
        {
            
            // Declaration hash-salt password
            byte[] passwordHash, passwordSalt;

            // Create hash-salt password of delivered password from application
            CreatePasswordHashSalt(password, out passwordHash, out passwordSalt);

            // Assign encrypt password before save to database
            employee.PasswordHash = passwordHash;
            employee.PasswordSalt = passwordSalt;

            //
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        /// <summary>
        /// Check if user with arrived username is exist
        /// </summary>
        /// <param name="username">username of the employee to register</param>
        /// <returns>True if employee with that username exist, otherwise false</returns>
        public async Task<bool> EmployeeExists(string username)
        {
            // If username exist in database
            if (await _context.Employees.AnyAsync(u => u.Username == username))
                return true;

            // Otherwise
            return false;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Created encrypt hash-salt password of password from application
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHashSalt(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Before login check if password is correct
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i])
                        return false;
                }

                return true;
            }
        }

        #endregion
    }
}
