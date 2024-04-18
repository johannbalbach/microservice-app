using AutoMapper;
using Enrollment.Domain.Models;
using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Exceptions;
using Shared.Interfaces;
using Shared.Models;
using Shared.Models.DTO;
using Shared.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enrollment.BL.Services
{
    public class EnrollmentService: IEnrollmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public EnrollmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult<Response>> AssignManagerToAdmission(Guid admissionId)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> AssignManagerToApplicant(Guid applicantId)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> EditAdmissionStatus(StatusEnum body, Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting(AdmissionsFilterQuery query)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> AddProgramToApplicantList(Guid id)
        {
            if (_IsUserInDb(user).Result)
            {
                throw new BadRequestException("user with that email is already in database");
            }

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = await LoginUser(new LoginCredentials
            {
                Email = user.Email,
                Password = user.Password,
            });

            return result;
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms()
        {
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
