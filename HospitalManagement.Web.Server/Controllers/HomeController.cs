using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalManagement.Web.Server
{
    public class HomeController : Controller
    {
        #region Protected Members

        /// <summary>
        /// The scoped Application context
        /// </summary>
        protected DataContext _Context;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        public HomeController(DataContext context)
        {
            _Context = context;
        }

        #endregion

        public IActionResult Index()
        {
            // Make sure we have the database
            _Context.Database.EnsureCreated();

            if (!_Context.Employees.Any())
            {
                _Context.Employees.Add(new Employee
                {
                    Username = "Username"
                });

                _Context.SaveChanges();
            }
            

            return View();
        }
    }
}
