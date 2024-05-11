using Document.Domain.Context;
using Document.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shared.DTO;
using Shared.DTO.ServiceBusDTO;
using Shared.Exceptions;
using Shared.Interfaces;
using Shared.Models;
using Shared.Models.Enums;
using System;
using Response = Shared.Models.Response;

namespace Document.BL.Services
{
    public class DocumentsService : IDocumentService
    {
        private readonly DocumentContext _context;
        private readonly IRequestClient<GetUserDTORequest> _getUserRequestClient;
        private readonly IRequestClient<GetManagerAccessBoolRequest> _getEnrollmentRequestClient;

        public DocumentsService(DocumentContext context, IBus bus)
        {
            _context = context;
            _getUserRequestClient = bus.CreateRequestClient<GetUserDTORequest>();
            _getEnrollmentRequestClient = bus.CreateRequestClient<GetManagerAccessBoolRequest>();
        }
        public async Task<ActionResult<Response>> AddApplicantEducationDocument(DocumentCreateDTO body, List<IFormFile> files, string email)
        {
            IsFilesImages(files);

            var educationDocument = new EducationDocument(body.Name, body.DocumentTypeId);
            educationDocument.ApplicantId = await IsUserApplicant(email);

            foreach (IFormFile file in files)
            {
                var fileModel = await SaveFile(file);

                educationDocument.FilesId.Add(fileModel.Id);
            }

            _context.EducationDocuments.Add(educationDocument);
            await _context.SaveChangesAsync();

            return new Response("Successfully added");
        }

        public async Task<ActionResult<Response>> AddApplicantPassport(PassportCreateDTO body, List<IFormFile> files, string email)
        {
            IsFilesImages(files);

            var passport = new Passport(body.SeriesNumber, body.IssuedBy, body.PlaceOfBirth, body.IssuedDate, body.BirthDate);
            passport.ApplicantId = await IsUserApplicant(email);

            foreach (IFormFile file in files)
            {
                var fileModel = await SaveFile(file);

                passport.FilesId.Add(fileModel.Id);
            }

            _context.Passports.Add(passport);
            await _context.SaveChangesAsync();

            return new Response("Successfully added");
        }

        public async Task<ActionResult<Response>> DeleteDocumentScan(Guid scanId, string email)
        {
            var existScan = await _context.fileDocuments.FirstOrDefaultAsync(a => a.Id == scanId);

            if (existScan == null)
                throw new BadRequestException("this scan doesnt exist");
            await CheckUserAccess(email, existScan);

            _context.Remove(existScan);
            await _context.SaveChangesAsync();

            return new Response("Scan successfully deleted");
        }

        public async Task<IActionResult> DownloadDocumentScan(Guid scanId, string email)
        {
            var existScan = await _context.fileDocuments.FirstOrDefaultAsync(a => a.Id == scanId);

            if (existScan == null)
                throw new BadRequestException("this scan doesnt exist");
            await CheckUserAccess(email, existScan);

            Console.WriteLine(existScan.Path);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existScan.Path);
            Console.WriteLine(filePath);

            if (!System.IO.File.Exists(filePath))
            {
                throw new NotFoundException("File not found on server.");
            }
            //??
            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return new FileContentResult(fileBytes, "application/octet-stream");
        }
        public async Task<ActionResult<Response>> UploadDocumentScan(Guid documentId, IFormFile file, string email)
        {
            var user = await GetUser(email);
            var document = await _context.EducationDocuments.FirstOrDefaultAsync(d => d.Id == documentId);
            var passport = await _context.Passports.FirstOrDefaultAsync(p => p.Id == documentId);

            if (document == null && passport == null)
                throw new BadRequestException("document or passport with that id doesnt exist");
            if (document != null)
            {
                await CheckUserAccessToDocument(document, user);
                await AddFileToDocument(document, file);
            }
            if (passport != null)
            {
                await CheckUserAccessToDocument(passport, user);
                await AddFileToDocument(passport, file);
            }
            return new Response("Successfully uploaded scan");
        }

        public async Task<ActionResult<Response>> EditPassport(PassportEditDTO body, string email)
        {
            var userId = await IsUserApplicant(email);

            return await _editPassport(body, userId);
        }
        public async Task<ActionResult<Response>> EditApplicantPassport(PassportEditDTO body, Guid applicantId, string email)
        {
            await CheckManagerAccess(applicantId, email);

            return await _editPassport(body, applicantId);
        }
        public async Task<ActionResult<Response>> EditEducationDocument(EducationDocumentEditDTO body, string email)
        {
            var userId = await IsUserApplicant(email);

            return await _editEducationDocument(body, userId);
        }
        public async Task<ActionResult<Response>> EditApplicantEducationDocument(EducationDocumentEditDTO body, Guid applicantId, string email)
        {
            await CheckManagerAccess(applicantId, email);

            return await _editEducationDocument(body, applicantId);
        }

        public async Task<ActionResult<EducationDocumentViewDTO>> GetEducationDocument(string email)
        {
            var userId = await IsUserApplicant(email);

            return await _getEducationDocument(userId);
        }

        public async Task<ActionResult<EducationDocumentViewDTO>> GetApplicantEducationDocument(Guid applicantId, string email)
        {
            await CheckManagerAccess(applicantId, email);

            return await _getEducationDocument(applicantId);
        }

        public async Task<ActionResult<PassportViewDTO>> GetPassport(string email)
        {
            var userId = await IsUserApplicant(email);

            return await _getPassport(userId);
        }

        public async Task<ActionResult<PassportViewDTO>> GetApplicantPassport(Guid applicantId, string email)
        {
            await CheckManagerAccess(applicantId, email);

            return await _getPassport(applicantId);
        }

        private async Task<PassportViewDTO> _getPassport(Guid userId)
        {
            var passport = await _context.Passports.FirstOrDefaultAsync(d => d.Id == userId);
            if (passport == null)
                throw new NotFoundException("this user havent any passport");

            return new PassportViewDTO
            {
                Id = passport.Id,
                ApplicantId = userId,
                SeriesNumber = passport.SeriesNumber,
                IssuedBy =  passport.IssuedBy,
                IssuedDate = passport.IssuedDate,
                BirthDate = passport.BirthDate,
                PlaceOfBirth = passport.PlaceOfBirth,
                FilesId = passport.FilesId
            };
        }
        private async Task<EducationDocumentViewDTO> _getEducationDocument(Guid userId)
        {
            var educationDocument = await _context.EducationDocuments.FirstOrDefaultAsync(d => d.Id == userId);
            if (educationDocument == null)
                throw new NotFoundException("this user havent any education document");

            return new EducationDocumentViewDTO
            {
                ApplicantId = userId,
                DocumentTypeId = educationDocument.DocumentTypeGuid,
                FilesId = educationDocument.FilesId,
                Id = educationDocument.Id,
                Name = educationDocument.Name
            };
        }
        private async Task<Response> _editPassport(PassportEditDTO body, Guid userId)
        {
            var existPassport = await _context.Passports.FirstOrDefaultAsync(p => p.ApplicantId == userId);
            if (existPassport == null)
                throw new NotFoundException("user with this guid dont have any passport");

            existPassport.BirthDate = body.BirthDate;
            existPassport.IssuedDate = body.IssuedDate;
            existPassport.PlaceOfBirth = body.PlaceOfBirth;
            existPassport.IssuedBy = body.IssuedBy;
            existPassport.SeriesNumber = body.SeriesNumber;

            await _context.SaveChangesAsync();
            return new Response("succesfulyy edited");
        }
        private async Task<Response> _editEducationDocument(EducationDocumentEditDTO body, Guid userId)
        {
            var existDocument = await _context.EducationDocuments.FirstOrDefaultAsync(p => p.ApplicantId == userId);
            if (existDocument == null)
                throw new NotFoundException("user with this guid dont have any education document");

            existDocument.DocumentTypeGuid = body.DocumentTypeId;
            existDocument.Name = body.Name;

            await _context.SaveChangesAsync();
            return new Response("succesfulyy edited");
        }
        private async Task<FileDocument> SaveFile(IFormFile file)
        {
            string fileName = Path.GetFileName(file.FileName);
            string path = $"/Files/{Guid.NewGuid()}{Path.GetExtension(fileName)}";

            using (var fileStream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", path), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var fileModel = new FileDocument { Path = path };
            _context.fileDocuments.Add(fileModel);
            await _context.SaveChangesAsync();

            return fileModel;
        }

        private void IsFilesImages(List<IFormFile> files)
        {
            foreach(IFormFile f in files)
            {
                if (!f.ContentType.StartsWith("image/"))
                    throw new BadRequestException("Only image files are allowed for education documents.");
            }
        }
        private async Task<Guid> IsUserApplicant(string email)
        {
            var user = await GetUser(email);

            if (!user.Roles.Contains(RoleEnum.Applicant))
                throw new ForbiddenException("Only applicants can upload documents");
            return user.Id;
        }
        private async Task AddFileToDocument(Document.Domain.Entities.Document document, IFormFile file)
        {
            if (!file.ContentType.StartsWith("image/"))
                throw new BadRequestException("Only image files are allowed for documents.");

            var fileModel = await SaveFile(file);
            document.FilesId.Add(fileModel.Id);

            await _context.SaveChangesAsync();
        }
        private async Task CheckUserAccessToDocument(Document.Domain.Entities.Document document, UserRights user)
        {
            if (document == null)
                throw new NotFoundException("document you try to access didnt exist");
            if (user.Roles.Contains(RoleEnum.Applicant))
            {
                if (document.ApplicantId != user.Id)
                    throw new ForbiddenException("you havent access to this scan");
            }
            else if (user.Roles.Contains(RoleEnum.Manager))
            {
                if (document != null)
                    if (await CheckAssign(document.ApplicantId, user.Id))
                        throw new ForbiddenException("you havent access to this scan");
            }
        }
        private async Task CheckUserAccess(string email, FileDocument scan)
        {
            var user = await GetUser(email);
            var document = await _context.EducationDocuments.FirstOrDefaultAsync(d => d.FilesId.Contains(scan.Id));
            var passport = await _context.Passports.FirstOrDefaultAsync(p => p.FilesId.Contains(scan.Id));

            if (document == null && passport == null)
                throw new BadRequestException("this scan not assigned to any document");
            if (document != null)
                await CheckUserAccessToDocument(document, user);
            if (passport != null)
                await CheckUserAccessToDocument(passport, user);
        }
        private async Task CheckManagerAccess(Guid applicantId, string email)
        {
            //applicant в принципе не сможет использовать этот метод из-за авторизации
            var applicant = await GetUser(applicantId);
            if (!(applicant.Roles.Contains(RoleEnum.Applicant)))
                throw new BadRequestException("user you try to edit is not applicant");

            var user = await GetUser(email);
            if (user.Roles.Contains(RoleEnum.Manager))
            {
                if (!(await CheckAssign(applicantId, user.Id)))
                    throw new ForbiddenException("you dont have enought rights to do this action");
            }
        }
        private async Task<bool> CheckAssign(Guid ApplicantId, Guid ManagerId)
        {
            //var response = await _getUserRequestClient.GetResponse<UserRights>(new GetUserDTO { Email = email });

            return true;
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
    }
}
