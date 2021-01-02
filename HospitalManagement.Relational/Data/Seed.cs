using HospitalManagement.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace HospitalManagement.Relational
{
    /// <summary>
    /// Read all data from <see cref="DataSeed.json"/> file and save in database
    /// </summary>
    public class Seed
    {
        #region Private Members

        private readonly DataContext _context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default contructor
        /// </summary>
        /// <param name="context">Inject db context which allow save data to database</param>
        public Seed ( DataContext context )
        {
            _context = context;
        }

        #endregion

        /// <summary>
        /// Deserialize object from file with seed data and save to database
        /// </summary>
        public void SeedEmployees ()
        {
            // If database is empty
            if( _context.Employees.Any() ) return;
            // Get all data from file
            var employeeData = File.ReadAllText( "Data/DataSeed.json" );

            // Deserialize getting data
            var employees = JsonConvert.DeserializeObject<List<Employee>>( employeeData );

            foreach (var employee in employees)
            {
                // create hash-salt for password from file
                byte[] passwordHash, passwordSalt;

                // Password is pesel if is the first employee login
                CreatePasswordHashSalt( employee.IsFirstLogin ? employee.Pesel : "Password", out passwordHash, out passwordSalt );

                employee.PasswordHash = passwordHash;
                employee.PasswordSalt = passwordSalt;
                employee.Username = employee.FirstName.Substring( 0, 1 ) + employee.LastName.Substring( 0, 1 ) + employee.Pesel[6..];

                _context.Employees.Add( employee );
            }

            _context.SaveChanges();
        }

        /// <summary>
        /// Created encrypt hash-salt password of password from application
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        private void CreatePasswordHashSalt ( string password, out byte[] passwordHash, out byte[] passwordSalt )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash( Encoding.UTF8.GetBytes( password ) );
            }
        }
    }
}
