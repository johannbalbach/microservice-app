using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Shared.Models;
using Shared.DTO;
using Shared.Interfaces;
using Shared.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Document.Application.Controllers
{
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentService _documentsService;

        public DocumentsController(IDocumentService documentsService)
        {
            _documentsService = documentsService;
        }

        [Authorize]
        [HttpPost]
        [Route("/documents/addEducationDocument")]
        public async Task<ActionResult<Response>> AddApplicantEducationDocument(List<IFormFile> files, [FromQuery] DocumentCreateDTO body)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _documentsService.AddApplicantEducationDocument(body, files, userEmailClaim);
        }

        [Authorize]
        [HttpPost]
        [Route("/documents/addPassport")]
        public async Task<ActionResult<Response>> AddApplicantPassport(List<IFormFile> files, [FromQuery] PassportCreateDTO body)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");

            return await _documentsService.AddApplicantPassport(body, files, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpDelete]
        [Route("/documents/deleteScan/{ScanId}")]
        public async Task<ActionResult<Response>> DeleteDocumentScan([FromRoute][Required] Guid scanId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.DeleteDocumentScan(scanId, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpGet]
        [Route("/documents/download/{ScanId}")]
        public async Task<IActionResult> DownloadDocumentScan([FromRoute][Required] Guid scanId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.DownloadDocumentScan(scanId, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpPost]
        [Route("/documents/upload/{documentId}")]
        public async Task<ActionResult<Response>> UploadDocumentScan(IFormFile file, [FromRoute][Required] Guid documentId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.UploadDocumentScan(documentId, file, userEmailClaim);
        }

        [Authorize]
        [HttpPut]
        [Route("/documents/editPassport/")]
        public async Task<ActionResult<Response>> EditPassport([FromQuery] PassportEditDTO body)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditPassport(body, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpPut]
        [Route("/documents/{applicantId}/editPassport/")]
        public async Task<ActionResult<Response>> EditApplicantPassport([FromQuery] PassportEditDTO body, [FromRoute][Required] Guid applicantId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditApplicantPassport(body, applicantId, userEmailClaim);
        }

        [Authorize]
        [HttpPut]
        [Route("/documents/editEducationDocument/")]
        public async Task<ActionResult<Response>> EditEducationDocument([FromQuery] EducationDocumentEditDTO body)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditEducationDocument(body, userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpPut]
        [Route("/documents/{applicantId}/editEducationDocument/")]
        public async Task<ActionResult<Response>> EditApplicantEducationDocument([FromQuery] EducationDocumentEditDTO body, [FromRoute][Required] Guid applicantId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.EditApplicantEducationDocument(body, applicantId, userEmailClaim);
        }

        [Authorize]
        [HttpGet]
        [Route("/documents/getEducationDocument/{DocumentTypeId}")]
        public async Task<ActionResult<EducationDocumentViewDTO>> GetEducationDocument([FromRoute][Required] Guid DoccumentTypeId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetEducationDocument(userEmailClaim, DoccumentTypeId);
        }

        [Authorize(Policy = "Privileged")]
        [HttpGet]
        [Route("/documents/{ApplicantId}/getEducationDocument/{DocumentTypeId}")]
        public async Task<ActionResult<EducationDocumentViewDTO>> GetApplicantEducationDocument([FromRoute][Required] Guid applicantId, [FromRoute][Required] Guid DoccumentTypeId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetApplicantEducationDocument(applicantId, userEmailClaim, DoccumentTypeId);
        }

        [Authorize]
        [HttpGet]
        [Route("/documents/getPassport")]
        public async Task<ActionResult<PassportViewDTO>> GetPassport()
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetPassport(userEmailClaim);
        }

        [Authorize(Policy = "Privileged")]
        [HttpGet]
        [Route("/documents/{ApplicantId}/getPassport")]
        public async Task<ActionResult<PassportViewDTO>> GetApplicantPassport([FromRoute][Required] Guid applicantId)
        {
            var userEmailClaim = "applicant@example.com";//HttpContext.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (userEmailClaim == null)
                throw new InvalidTokenException("Token not found");
            return await _documentsService.GetApplicantPassport(applicantId, userEmailClaim);
        }
    }
}
