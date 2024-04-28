using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.Domain.Models;
using Enrollment.Domain.Models.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
using System.Xml.Linq;

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
        public async Task<ActionResult<Response>> AssignManagerToAdmission(Guid admissionId, Guid managerId)
        {
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.Id == admissionId);

            if (await isUserExist(managerId))
                throw new BadRequestException("this manager doesnt exist");
            if (existAdmission == null)
                throw new BadRequestException("this admissions doesnt exist");
/*            if (existAdmission.ManagerId == managerId)
                throw new BadRequestException("this manager is already assigned to this admission");*/

            existAdmission.ManagerId = managerId;
            await _context.SaveChangesAsync();

            return new Response("manager successfully assigned to this admission");
        }
        public async Task<ActionResult<Response>> AssignManagerToApplicant(Guid applicantId, Guid managerId)
        {
            var existAdmissions = await _context.Admissions.Where(a => a.ApplicantId == applicantId).ToListAsync();

            if (await isUserExist(applicantId))
                throw new BadRequestException("this applicant doesnt exist");
            if (existAdmissions == null)
                throw new BadRequestException("this user have no admissions");

            existAdmissions.ForEach(async a => await AssignManagerToAdmission(a.Id, managerId));

            return new Response("manager successfully assigned to all admissions of this applicant");
        }
        public async Task<ActionResult<Response>> EditAdmissionStatus(StatusEnum body, Guid id)
        {
            var userId = await GetUserId();
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.Id == id);

            if (existAdmission == null)
            {
                throw new BadRequestException("this admission doesnt exist");
            }

            existAdmission.Status = body;
            await _context.SaveChangesAsync();

            return new Response("status successfully changed");
        }
        public async Task<ActionResult<List<AdmissionDTO>>> GetApplicantAdmissions(Guid id)
        {
            var userId = await GetUserId();

            var admissions = await _context.Admissions.Where(a => a.ApplicantId == userId).ToListAsync();

            var result = _mapper.Map<List<Admission>, List<AdmissionDTO>>(admissions);

            return result;
        }
        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting(AdmissionsFilterQuery query)
        {
            var admissionsQuery = _context.Admissions.AsQueryable();

            if (query.ProgramId != null)
            {
                admissionsQuery = admissionsQuery.Where(a => a.ProgramId == query.ProgramId);
            }
            if (query.FacultyId != null)
            {
                //admissionsQuery = admissionsQuery.Where(a => a.P);
            }
            if (query.Status != null)
            {
                admissionsQuery = admissionsQuery.Where(a => a.Status == query.Status);
            }
            if (query.UnassignedOnly == true)
            {
                admissionsQuery = admissionsQuery.Where(a => a.ManagerId == null);
            }

/*            if (!string.IsNullOrEmpty(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "createdtime":
                        admissionsQuery = admissionsQuery.OrderBy(a => a.CreatedTime);
                        break;
                    case "priority":
                        admissionsQuery = admissionsQuery.OrderBy(a => a.Priority);
                        break;
                    default:
                        break;
                }
            }*/

            var pageNumber = query.Page ?? 1;
            var pageSize = query.Size ?? 10;
            var totalCount = await admissionsQuery.CountAsync();
            var admissions = await admissionsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var admissionDTOs = admissions.Select(a => new AdmissionDTO
            {
                Id = a.Id,
                ApplicantId = a.ApplicantId,
                CreatedTime = a.CreatedTime,
                ProgramId = a.ProgramId,
                Priority = a.Priority,
                Status = a.Status,
                ManagerId = a.ManagerId
            }).ToList();

            var admissionWithPaginationInfo = new AdmissionWithPaginationInfo
            {
                Admissions = admissionDTOs,
                elementsCount = totalCount,
                pageCurrent = pageNumber,
                size = pageSize
            };

            return admissionWithPaginationInfo;
        }
        public async Task<ActionResult<Response>> AddProgramToApplicantList(Guid id)
        {
            var userId = await GetUserId();
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.ProgramId == id && a.ApplicantId == userId);
            
            if (existAdmission != null)
            {
                throw new BadRequestException("this admission is already exist");
            }

            var admission = new Admission{
                Id = Guid.NewGuid(),
                ApplicantId = userId,
                CreatedTime = DateTime.UtcNow,
                ProgramId = id,
                Priority = await CalculatePriority(),
                Status = StatusEnum.Created,
            };

            await _context.Admissions.AddAsync(admission);
            await _context.SaveChangesAsync();

            return new Response("Admission successfully created");
        }
        public async Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid id)
        {
            var userId = await GetUserId();
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.Id == id);

            if (existAdmission == null)
            {
                throw new BadRequestException("this admission doesnt exist");
            }

            existAdmission.Priority = priority;

            var userAdmissions = await _context.Admissions.Where(a => a.ApplicantId == userId).ToListAsync();
            userAdmissions.ForEach(async a => a.Priority = await CalculatePriority());

            await _context.SaveChangesAsync();

            return new Response("priority successfully changed");
        }
        public async Task<ActionResult<List<ApplicantProgramDTO>>> GetApplicantPrograms()
        {
            var userId = await GetUserId();

            var admissions = await _context.Admissions.Where(a => a.ApplicantId == userId).ToListAsync();

            //var result = ;
            throw new NotImplementedException();
        }
        public async Task<ActionResult<Response>> RemoveProgramFromApplicantList(Guid id)
        {
            var userId = await GetUserId();
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.ProgramId == id && a.ApplicantId == userId);

            if (existAdmission == null)
            {
                throw new BadRequestException("this admission doesnt exist");
            }

            _context.Remove(existAdmission);
            await _context.SaveChangesAsync();

            return new Response("Admission successfully removed");
        }

        private async Task<Guid> GetUserId()
        {
            throw new BadRequestException("this user is not applicant");
            throw new BadRequestException("user doesnt exist");
            //add logic
            return Guid.NewGuid();
        }
        private async Task<int> CalculatePriority()
        {
            //add logic
            return 0;
        }
        private async Task<bool> isUserExist(Guid Id)
        {
            throw new BadRequestException("this user is not applicant");
            throw new BadRequestException("user doesnt exist");
            //add logic
            return true;
        }
    }
}
