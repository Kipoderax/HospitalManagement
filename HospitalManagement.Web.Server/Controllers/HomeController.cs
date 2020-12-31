using HospitalManagement.Relational;
using Microsoft.AspNetCore.Mvc;

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
            _Context.Database.EnsureCreated();

            return View();
        }
    }
}
