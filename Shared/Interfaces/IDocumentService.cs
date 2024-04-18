using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Models;

namespace Shared.Interfaces
{
    public interface IDocumentService
    {
        Task<ActionResult<Response>> AddApplicantEducationDocument(DocumentCreateDTO body);
        Task<ActionResult<Response>> AddApplicantPassport(PassportCreateDTO body);
        Task<ActionResult<Response>> DeleteDocumentScan(Guid? scanId);
        Task<ActionResult<Response>> DownloadDocumentScan(Guid? scanId);
        Task<ActionResult<Response>> EditDocument(Guid? documentId);
        Task<ActionResult<Response>> GetEducationDocument();
        Task<ActionResult<Response>> GetApplicantEducationDocument(Guid? applicantId);
        Task<ActionResult<Response>> GetPassport();
        Task<ActionResult<Response>> GetApplicantPassport(Guid? applicantId);
    }
}
