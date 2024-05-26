using Document.Domain.Entities;
using Shared.DTO;
using AutoMapper;

namespace Document.BL.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DocumentCreateDTO, EducationDocument>();
        }
    }
}
