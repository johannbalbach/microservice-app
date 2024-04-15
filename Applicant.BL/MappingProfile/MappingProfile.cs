using Applicant.Domain.Entities;
using AutoMapper;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicant.BL.MappingProfile
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Faculty, FacultyDTO>();
        }
    }
}
