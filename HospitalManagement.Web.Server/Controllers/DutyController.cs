using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HospitalManagement.Core;
using HospitalManagement.Relational;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace HospitalManagement.Web.Server
{
    [ApiController]
    [Route("api/duties/")]
    [AuthorizeToken]
    public class DutyController : ControllerBase
    {
        #region Private Members

        private readonly IDutyRepository _dutyRepository;
        private readonly IConfiguration _config;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public DutyController(IDutyRepository dutyRepository,
            IConfiguration config,
            DataContext context,
            IMapper mapper)
        {
            _dutyRepository = dutyRepository;
            _config = config;
            _context = context;
            _mapper = mapper;
        }
        
        #endregion
        
        [Route("{username}")]
        public async Task<ApiResponse<IEnumerable<DutyResultApiModel>>> GetAllEmployees(string username)
        {
            var employeeDuties = await _dutyRepository.GetEmployeeDutiesByUsername(username);

            var employeeDutyApi = _mapper.Map<IEnumerable<DutyResultApiModel>> ( employeeDuties );
            
            return new ApiResponse<IEnumerable<DutyResultApiModel>>
            {
                Response = employeeDutyApi
            };
        }
    }
}