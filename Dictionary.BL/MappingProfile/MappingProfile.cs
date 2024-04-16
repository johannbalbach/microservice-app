using AutoMapper;
using Dictionary.Domain.Entities;
using Shared.DTO;

namespace Dictionary.BL.MappingProfile
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Faculty, FacultyDTO>();
        }
    }
}
