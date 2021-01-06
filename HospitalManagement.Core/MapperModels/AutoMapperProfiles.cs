using AutoMapper;

namespace HospitalManagement.Core
{
    /// <summary>
    /// Converter data models to api models
    /// NOTE: More information - https://docs.automapper.org
    /// </summary>
    public class AutoMapperProfiles : Profile
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AutoMapperProfiles()
        {
            EmployeeForEmployeeResultApiModel();
            EmployeeForDutyResultApiModel();
        }

        #endregion

        /// <summary>
        /// Convert employee model to employee dto
        /// </summary>
        private void EmployeeForEmployeeResultApiModel()
        {
            
            CreateMap<Employee, EmployeeResultApiModel>()
                .ForMember (
                    dest => dest.EmployeeSpecialize.SpecializeEmployee,
                     opt => opt.MapFrom ( src => src.EmployeeSpecialize.SpecializeEmployee ) )
                .ForMember (
                    dest => dest.EmployeeType,
                    opt => { opt.MapFrom ( src => src.EmployeeType.EmployeeRole ); } );
            
        }

        private void EmployeeForDutyResultApiModel()
        {
            CreateMap<Duty, DutyDto>()
                .ForMember (
                    dest => dest.Employee.FirstName,
                    opt => opt.MapFrom ( src => src.Employee.FirstName ) )
                .ForMember (
                    dest => dest.Employee.EmployeeSpecialize,
                    opt => opt.MapFrom ( src => src.Employee.EmployeeSpecialize ) );
            
            Mapper.AssertConfigurationIsValid();
        }
    }
}