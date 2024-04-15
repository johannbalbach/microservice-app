using AutoMapper;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Entities;

namespace User.BL.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserE, ApplicantProfileDTO>();
            CreateMap<UserE, ApplicantRegisterDTO>();
            CreateMap<UserE, ApplicantProfileEditDTO>();
            CreateMap<UserE, LoginCredentials>();
            CreateMap<UserE, UserProfileDTO>();
            CreateMap<UserE, UserProfileEditDTO>();
            CreateMap<UserE, UserRegisterDTO>();
            CreateMap<UserRegisterDTO, UserE>();
        }
    }
}
