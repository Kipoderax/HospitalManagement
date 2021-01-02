using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using HospitalManagement.Relational;
using HospitalManagement.Core;
using Microsoft.AspNetCore.Authorization;

namespace HospitalManagement.Web.Server
{
    [Route("api/[controller]")]
    [AuthorizeToken]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        #region Private Members

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public EmployeeController(IEmployeeRepository employeeRepository,
                                  IConfiguration config,
                                  DataContext context,
                                  IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _config = config;
            _context = context;
            _mapper = mapper;
        }

        #endregion

        [HttpGet("employees")]
        public async Task<ApiResponse<IEnumerable<EmployeeResultApiModel>>> GetAllEmployees()
        {
            var employee = await _employeeRepository.GetEmployees();

            var employeeApi = _mapper.Map<IEnumerable<EmployeeResultApiModel>> ( employee );
            
            return new ApiResponse<IEnumerable<EmployeeResultApiModel>>
            {
                Response = employeeApi
            };
        }
    }
}