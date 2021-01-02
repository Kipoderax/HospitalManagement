using AutoMapper;
using System.Linq;

namespace HospitalManagement.Core
{
    /// <summary>
    /// Converter data models to api models
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
            CreateMap<Duty, DutyResultApiModel>()
                .ForMember (
                    dest => dest.StartShift,
                    opt => { opt.MapFrom ( src => src.StartShift ); } )
                .ForMember (
                    dest => dest.EndShift,
                    opt => { opt.MapFrom ( src => src.EndShift ); } );
        }
    }
}