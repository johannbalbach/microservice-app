using AutoMapper;
using Enrollment.Domain.Entities;
using Shared.DTO;
using Shared.Models.DTO;

namespace Enrollment.BL.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Admission, AdmissionDTO>();
        }
    }
}
