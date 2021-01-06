using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalManagement.Core;
using HospitalManagement.Relational;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ApiResponse<IEnumerable<DutyDto>>> GetAllEmployeeDutiesAsync(string username)
        {
            var employeeDuties = await _dutyRepository.GetEmployeeDutiesByUsername(username);

            var employeeDutyApi = _mapper.Map<List<DutyDto>> ( employeeDuties );
            
            return new ApiResponse<IEnumerable<DutyDto>>
            {
                Response = employeeDutyApi
            };
        }

        [AllowAnonymous]
        public async Task<ApiResponse<IEnumerable<DutyDto>>> GetAllDutiesAsync()
        {
            var duties = await _dutyRepository.GetEmployeeDuties();

            var dutiesApi = _mapper.Map<List<DutyDto>> ( duties );
            
            

            return new ApiResponse<IEnumerable<DutyDto>>
            {
                Response = dutiesApi
            };
        }
    }
}