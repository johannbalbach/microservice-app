using System.ComponentModel.DataAnnotations;
using Shared.Models;
using Shared.DTO;
using Shared.Interfaces;
using Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Document.Application.Controllers
{
    [ApiController]
    [Route("api/documents/")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentsService;

        public DocumentsController(IDocumentService documentsService)
        {
            _documentsService = documentsService;
        }

        [Authorize]
        [HttpPost]
        [Route("addEducationDocument")]
        public async Task<ActionResult<Response>> AddApplicantEducationDocument(List<IFormFile> files, [FromQuery] DocumentCreateDTO body)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _documentsService.AddApplicantEducationDocument(body, files, userEmailClaim);
        }
        
        [HttpPost]
        [Authorize]
        [Route("addPassport")]
        public async Task<ActionResult<Response>> AddApplicantPassport(List<IFormFile> files, [FromQuery] PassportCreateDTO body)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _documentsService.AddApplicantPassport(body, files, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpDelete]
        [Route("deleteScan/{ScanId}")]
        public async Task<ActionResult<Response>> DeleteDocumentScan([FromRoute][Required] Guid ScanId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.DeleteDocumentScan(ScanId, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpGet]
        [Route("download/{ScanId}")]
        public async Task<IActionResult> DownloadDocumentScan([FromRoute][Required] Guid ScanId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.DownloadDocumentScan(ScanId, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpPost]
        [Route("upload/{documentId}")]
        public async Task<ActionResult<Response>> UploadDocumentScan(IFormFile file, [FromRoute][Required] Guid documentId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.UploadDocumentScan(documentId, file, userEmailClaim);
        }

        [Authorize]
        [HttpPut]
        [Route("editPassport/")]
        public async Task<ActionResult<Response>> EditPassport([FromQuery] PassportEditDTO body)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditPassport(body, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpPut]
        [Route("{applicantId}/editPassport/")]
        public async Task<ActionResult<Response>> EditApplicantPassport([FromQuery] PassportEditDTO body, [FromRoute][Required] Guid applicantId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditApplicantPassport(body, applicantId, userEmailClaim);
        }

        [Authorize]
        [HttpPut]
        [Route("editEducationDocument/")]
        public async Task<ActionResult<Response>> EditEducationDocument([FromQuery] EducationDocumentEditDTO body)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditEducationDocument(body, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpPut]
        [Route("{applicantId}/editEducationDocument/")]
        public async Task<ActionResult<Response>> EditApplicantEducationDocument([FromQuery] EducationDocumentEditDTO body, [FromRoute][Required] Guid applicantId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditApplicantEducationDocument(body, applicantId, userEmailClaim);
        }

        [Authorize]
        [HttpGet]
        [Route("getEducationDocument/{DocumentTypeId}")]
        public async Task<ActionResult<EducationDocumentViewDTO>> GetEducationDocument([FromRoute][Required] Guid DocumentTypeId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetEducationDocument(userEmailClaim, DocumentTypeId);
        }

        [Authorize(Policy = "Privileged")]
        [HttpGet]
        [Route("{ApplicantId}/getEducationDocument/{DocumentTypeId}")]
        public async Task<ActionResult<EducationDocumentViewDTO>> GetApplicantEducationDocument([FromRoute][Required] Guid ApplicantId, [FromRoute][Required] Guid DocumentTypeId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetApplicantEducationDocument(ApplicantId, userEmailClaim, DocumentTypeId);
        }

        [Authorize]
        [HttpGet]
        [Route("getPassport")]
        public async Task<ActionResult<PassportViewDTO>> GetPassport()
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetPassport(userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpGet]
        [Route("{ApplicantId}/getPassport")]
        public async Task<ActionResult<PassportViewDTO>> GetApplicantPassport([FromRoute][Required] Guid ApplicantId)
        {
            var userEmailClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetApplicantPassport(ApplicantId, userEmailClaim);
        }
    }
}
