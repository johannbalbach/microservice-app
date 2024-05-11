using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IDocumentService
    {
        Task<ActionResult<Response>> AddApplicantEducationDocument(DocumentCreateDTO body, List<IFormFile> file, string email);
        Task<ActionResult<Response>> AddApplicantPassport(PassportCreateDTO body, List<IFormFile> file, string email);
        Task<ActionResult<Response>> DeleteDocumentScan(Guid scanId, string email);
        Task<IActionResult> DownloadDocumentScan(Guid scanId, string email);
        Task<ActionResult<Response>> UploadDocumentScan(Guid documentId, IFormFile file, string email);
        Task<ActionResult<Response>> EditPassport (PassportEditDTO body, string email);
        Task<ActionResult<Response>> EditApplicantPassport(PassportEditDTO body, Guid ApplicantId, string email);
        Task<ActionResult<Response>> EditEducationDocument(EducationDocumentEditDTO body, string email);
        Task<ActionResult<Response>> EditApplicantEducationDocument(EducationDocumentEditDTO body, Guid ApplicantId, string email);
        Task<ActionResult<EducationDocumentViewDTO>> GetEducationDocument(string email);
        Task<ActionResult<EducationDocumentViewDTO>> GetApplicantEducationDocument(Guid applicantId, string email);
        Task<ActionResult<PassportViewDTO>> GetPassport(string email);
        Task<ActionResult<PassportViewDTO>> GetApplicantPassport(Guid applicantId, string email);
    }
}
