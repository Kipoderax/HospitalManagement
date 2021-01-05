using System.Collections.Generic;
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

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dataContext">The injected context</param>
        public DutyRepository(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }
        
        #endregion
        
        /// <summary>
        /// Get list duties of the specify employee
        /// </summary>
        /// <param name="username">The employee username</param>
        /// <returns></returns>
        public async Task<IEnumerable<Duty>> GetEmployeeDutiesByUsername( string username )
        {
            var duty = await _dataContext.Duties
                    
                // filter by logged employee
                .Where( u => u.Employee.Username == username )
                .ToListAsync();

            return duty;
        }
    }
}