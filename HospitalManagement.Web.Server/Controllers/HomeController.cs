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

        private readonly IAuthRepository _authRepository;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="context">The injected context</param>
        /// <param name="authRepository">Repozytorium autentykacyjne</param>
        public HomeController(DataContext context, IAuthRepository authRepository)
        {
            _Context = context;
            _authRepository = authRepository;
        }

        #endregion

        public IActionResult Index()
        {
            Employee employee = new Employee()
            {
                Username = "Username"
            };

            _authRepository.Register( employee, "password" );

            return View();
        }
    }
}
