using AutoMapper;
using Enrollment.Domain.Entities;
using Enrollment.Domain.Models;
using Enrollment.Domain.Models.Enum;
using Enrollment.Domain.Models.Query;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.ServiceBusDTO;
using Shared.Enums;
using Shared.Exceptions;
using Shared.Interfaces;
using Shared.Models.DTO;
using Shared.Models.Enums;
using System;
using User.BL.Services;
using User.Domain.Entities;
using Response = Shared.Models.Response;

namespace Enrollment.BL.Services
{
    public class EnrollmentService: IEnrollmentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRequestClient<GetUserDTORequest> _getUserRequestClient;
        private readonly IRequestClient<GetProgramExistBoolRequest> _getDictionaryRequestClient;
        private readonly IRequestClient<AddAttributeToUserRequest> _addAttributeToUserClient;

        public EnrollmentService(AppDbContext context, IMapper mapper, IBus bus)
        {
            _context = context;
            _mapper = mapper;
            _getUserRequestClient = bus.CreateRequestClient<GetUserDTORequest>();
            _getDictionaryRequestClient = bus.CreateRequestClient<GetProgramExistBoolRequest>();
            _addAttributeToUserClient = bus.CreateRequestClient<AddAttributeToUserRequest>();
        }
        public async Task<ActionResult<Response>> AssignManagerToAdmission(Guid admissionId, Guid managerId)
        {
            var user = await GetUser(managerId);
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.Id == admissionId);
            
            if (!user.Roles.Contains(RoleEnum.Manager))
                throw new BadRequestException("this user is not manager");
            if (existAdmission == null)
                throw new BadRequestException("this admissions doesnt exist");
            if (existAdmission.ManagerId != null)
                if (existAdmission.ManagerId == managerId)
                    return new Response("this manager is already assigned to this admission");

            existAdmission.ManagerId = managerId;
            await _context.SaveChangesAsync();

            return new Response("manager successfully assigned to this admission");
        }
        public async Task<ActionResult<Response>> AssignManagerToApplicant(Guid applicantId, Guid managerId)
        {
            var applicant = await GetUser(applicantId);
            var manager = await GetUser(managerId);
            var existAdmissions = await _context.Admissions.Where(a => a.ApplicantId == applicantId).ToListAsync();

            if (!applicant.Roles.Contains(RoleEnum.Applicant))
                throw new BadRequestException("this user is not applicant");
            if (!manager.Roles.Contains(RoleEnum.Manager))
                throw new BadRequestException("this user is not manager");
            if (existAdmissions == null)
                throw new BadRequestException("this user have no admissions");

            existAdmissions.ForEach(async a => await AssignManagerToAdmission(a.Id, managerId));

            return new Response("manager successfully assigned to all admissions of this applicant");
        }
        public async Task<ActionResult<Response>> EditAdmissionStatus(StatusEnum body, Guid id, string email)
        {
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
            var admissions = await _context.Admissions.Where(a => a.ApplicantId == id).ToListAsync();
            if (admissions == null)
                throw new BadRequestException("this user didnt exist or he havent any admission");

            var admissionsDTO = new List<AdmissionDTO>();

            foreach (var adm in admissions)
            {
                var admDTO = _mapper.Map<AdmissionDTO>(adm);

                admissionsDTO.Add(admDTO);
            }
            return admissionsDTO;
        }
        public async Task<ActionResult<AdmissionWithPaginationInfo>> GetListOfAdmissionsWithPaginationFilteringAndSorting(AdmissionsFilterQuery query)
        {
            var admissionsQuery = _context.Admissions.AsQueryable();

            if (query.ProgramId != null)
            {
                admissionsQuery = admissionsQuery.Where(a => a.ProgramId == query.ProgramId);
            }
            if (query.Status != null)
            {
                admissionsQuery = admissionsQuery.Where(a => a.Status == query.Status);
            }
            if (query.UnassignedOnly == true)
            {
                admissionsQuery = admissionsQuery.Where(a => a.ManagerId == null);
            }
            if (query.FirstPriorityOnly == true)
            {
                admissionsQuery = admissionsQuery.Where(a => a.Priority == 1);
            }

            if (query.SortBy != null)
            {
                admissionsQuery = query.SortBy switch
                {
                    SortEnum.CreatedTimeAsc => admissionsQuery.OrderBy(a => a.CreatedTime),
                    SortEnum.CreatedTimeDesc => admissionsQuery.OrderByDescending(a => a.CreatedTime),
                    _ => admissionsQuery.OrderBy(a => a.CreatedTime)
                };
            }

            var pageNumber = query.Page ?? 1;
            var pageSize = query.Size ?? 10;
            var totalCount = await admissionsQuery.CountAsync();
            var admissions = await admissionsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var admissionsDtos = new List<AdmissionDTO>();
            foreach (var adm in admissions)
            {
                var admDto = _mapper.Map<AdmissionDTO>(adm);

                admissionsDtos.Add(admDto);
            }


            var admissionWithPaginationInfo = new AdmissionWithPaginationInfo
            {
                Admissions = admissionsDtos,
                elementsCount = totalCount,
                pageCurrent = pageNumber,
                size = pageSize
            };

            return admissionWithPaginationInfo;
        }
        public async Task<ActionResult<Response>> AddProgramToMyList(Guid id, string email)
        {
            var user = await GetUser(email);
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.ProgramId == id && a.ApplicantId == user.Id);
            if (!await IsProgramExist(id))
                throw new NotFoundException("Program with that id doesnt exist");
            
            if (existAdmission != null)
                throw new BadRequestException("this admission is already exist");

            var admission = new Admission{
                Id = Guid.NewGuid(),
                ApplicantId = user.Id,
                CreatedTime = DateTime.UtcNow,
                ProgramId = id,
                Priority = await MaxPriority(user.Id) + 1,
                Status = StatusEnum.Created,
            };

            await _context.Admissions.AddAsync(admission);
            await _context.SaveChangesAsync();

            return new Response("Admission successfully created");
        }
        public async Task<ActionResult<Response>> ChangeProgramPriority(int priority, Guid id, string email)
        {
            var user = await GetUser(email);
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.Id == id);

            if (priority <= 0)
                throw new BadRequestException("priority should be > 0");
            if (await _context.Admissions.Where(a => a.ApplicantId == user.Id).CountAsync() == null)
                throw new BadRequestException("you dont have any admission");
            if (priority > await _context.Admissions.Where(a => a.ApplicantId == user.Id).CountAsync())
                throw new BadRequestException("priority should be < then max admissions count");
            if (existAdmission == null)
                throw new BadRequestException("this admission doesnt exist");
            if (existAdmission.ManagerId != user.Id || user.Roles.Contains(RoleEnum.MainManager) || user.Roles.Contains(RoleEnum.Admin))
                throw new BadRequestException("you havent enough rights to edit this admission");

            existAdmission.Priority = priority;

            var userAdmissions = await _context.Admissions.Where(a => a.ApplicantId == existAdmission.ApplicantId && a.Id != existAdmission.Id && a.Priority>= priority).ToListAsync();
            if (userAdmissions != null)
                userAdmissions.ForEach(a => a.Priority = priority+1);

            await _context.SaveChangesAsync();

            return new Response("priority successfully changed");
        }
        public async Task<ActionResult<List<AdmissionDTO>>> GetMyAdmissions(string email)
        {
            var user = await GetUser(email);

            var admissions = await _context.Admissions.Where(a => a.ApplicantId == user.Id).ToListAsync();
            var admissionsDtos = new List<AdmissionDTO>();
            foreach (var adm in admissions)
            {
                var admDto = _mapper.Map<AdmissionDTO>(adm);

                admissionsDtos.Add(admDto);
            }

            return admissionsDtos;
        }
        public async Task<ActionResult<Response>> RemoveAdmissionFromApplicantList(Guid id, string email)
        {
            var user = await GetUser(email);
            var existAdmission = await _context.Admissions.FirstOrDefaultAsync(a => a.ProgramId == id);

            if (existAdmission == null)
                throw new BadRequestException("this admission doesnt exist");
            if (!(user.Roles.Contains(RoleEnum.MainManager) || user.Roles.Contains(RoleEnum.Admin)))
            {
                if (existAdmission.ApplicantId != user.Id && existAdmission.ManagerId != user.Id)
                    throw new BadRequestException("you haven enough rights to remove this admission");
            }

            _context.Remove(existAdmission);
            await _context.SaveChangesAsync();

            return new Response("Admission successfully removed");
        }

        private async Task AddAdmissionToUser(Guid userId, Guid admissionId)
        {

        }
        private async Task<bool> IsProgramExist(Guid id)
        {
            var response = await _getDictionaryRequestClient.GetResponse<ProgramExistBool>(new GetProgramExistBoolRequest { ProgramId = id });

            return response.Message.Exist;
        }
        private async Task<UserRights> GetUser(string email)
        {
            var response = await _getUserRequestClient.GetResponse<UserRights>(new GetUserDTORequest { Email = email });

            return response.Message;
        }
        private async Task<UserRights> GetUser(Guid id)
        {
            var response = await _getUserRequestClient.GetResponse<UserRights>(new GetUserDTORequest { UserId = id });
            return response.Message;
        }
        private async Task<int> MaxPriority(Guid id)
        {
           return await _context.Admissions
                .Where(a => a.ApplicantId == id)
                .Select(a => a.Priority)
                .DefaultIfEmpty(0)
                .MaxAsync();
        }
    }
}
