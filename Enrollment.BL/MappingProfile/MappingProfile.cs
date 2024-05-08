using AutoMapper;
using Enrollment.Domain.Entities;
using Shared.DTO;
using Shared.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
