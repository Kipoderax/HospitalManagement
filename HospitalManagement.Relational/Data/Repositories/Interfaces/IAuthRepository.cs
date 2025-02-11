﻿using HospitalManagement.Core;
using System.Threading.Tasks;

namespace HospitalManagement.Relational
{
    public interface IAuthRepository
    {
        /// <summary>
        /// Login employee to application
        /// </summary>
        /// <param name="username">Unique username</param>
        /// <param name="password">Employee password</param>
        /// <returns>login employee</returns>
        Task<Employee> LoginAsync(string username, string password);

        /// <summary>
        /// Register new employee to hospital application
        /// </summary>
        /// <param name="employee">Set data of employee to register</param>
        /// <param name="password">Password of the employee</param>
        /// <returns>new employee</returns>
        Task<Employee> RegisterAsync(Employee employee, string password);


        Task<bool> EmployeeExistsAsync(string username);
    }
}
