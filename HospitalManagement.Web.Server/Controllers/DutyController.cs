using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HospitalManagement.Core;
using HospitalManagement.Relational;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagement.Web.Server
{
    [ApiController]
    [Route("api/duties/")]
    [AuthorizeToken]
    public class DutyController : ControllerBase
    {
        #region Private Members

        private readonly IDutyRepository _dutyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public DutyController(IDutyRepository dutyRepository,
            IEmployeeRepository employeeRepository,
            DataContext context,
            IMapper mapper)
        {
            _dutyRepository = dutyRepository;
            _employeeRepository = employeeRepository;
            _context = context;
            _mapper = mapper;
        }
        
        #endregion
        
        [Route("{username}")]
        public async Task<ApiResponse<IEnumerable<DutyDto>>> GetAllEmployeeDutiesAsync(string username)
        {
            var employeeDuties = await _dutyRepository.FindEmployeeDutiesByUsernameAsync(username);

            var employeeDutyApi = _mapper.Map<List<DutyDto>> ( employeeDuties );
            
            return new ApiResponse<IEnumerable<DutyDto>>
            {
                Response = employeeDutyApi
            };
        }

        public async Task<ApiResponse<IEnumerable<DutyDto>>> GetAllDutiesAsync()
        {
            var duties = await _dutyRepository.GetEmployeeDutiesAsync();

            var dutiesApi = _mapper.Map<List<DutyDto>> ( duties );

            return new ApiResponse<IEnumerable<DutyDto>>
            {
                Response = dutiesApi
            };
        }

        [Route ( "add" )]
        public async Task<ApiResponse<DutyDto>> RegisterAsync( DutyDto dutyDto )
        {
            // If something was wrong
            if( !await _dutyRepository.AddDutyAsync ( dutyDto ) )
                // return ApiResponse with described errors
                // TODO: Add error message
                return new ApiResponse<DutyDto>();

            // Otherwise return new duty object
            return new ApiResponse<DutyDto>
            {
                Response = new DutyDto
                {
                    StartShift = dutyDto.StartShift,
                    EndShift = dutyDto.EndShift,
                    Employee = dutyDto.Employee
                }
            };
        }

        [Route ("edit")]
        public async Task<ApiResponse<DutyDto>> UpdateAsync( DutyDto dutyDto )
        {
            
            var employee = await _employeeRepository.GetEmployeeByUsernameAsync ( dutyDto.Employee.Username );
            var dutyToUpdate = await _dutyRepository.FindEmployeeDutyByStartShiftAndUsernameAsync ( dutyDto );
            
            if (dutyDto.StartShift == dutyDto.Employee.EmployeeDuties.First().StartShift)
                return new ApiResponse<DutyDto>();

            dutyToUpdate.StartShift = dutyDto.StartShift;
            dutyToUpdate.EndShift = dutyDto.EndShift;
            dutyToUpdate.Employee.UserId = employee.UserId;

            _dutyRepository.Update ( dutyToUpdate );
            await _context.SaveChangesAsync();

            return new ApiResponse<DutyDto>
            {
                Response = new DutyDto
                {
                    StartShift = dutyDto.StartShift,
                    EndShift = dutyDto.EndShift,
                    Employee = dutyDto.Employee
                }
            };
        }
    }
}