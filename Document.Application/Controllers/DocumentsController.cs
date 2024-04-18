using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Shared.Models;
using Shared.DTO;
using Shared.Interfaces;

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

        [HttpPost]
        [Route("/documents/addEducationDocument")]
        public async Task<ActionResult<Response>> AddApplicantEducationDocument([FromBody] DocumentCreateDTO body)
        {
            return await _documentsService.AddApplicantEducationDocument(body);
        }

        [HttpPost]
        [Route("/documents/addPassport")]
        public async Task<ActionResult<Response>> AddApplicantPassport([FromBody] PassportCreateDTO body)
        {
            return await _documentsService.AddApplicantPassport(body);
        }

        [HttpDelete]
        [Route("/documents/deleteScan/{ScanId}")]
        public async Task<ActionResult<Response>> DeleteDocumentScan([FromRoute][Required] Guid? scanId)
        {
            return await _documentsService.DeleteDocumentScan(scanId);
        }

        [HttpGet]
        [Route("/documents/download/{ScanId}")]
        public async Task<ActionResult<Response>> DownloadDocumentScan([FromRoute][Required] Guid? scanId)
        {
            return await _documentsService.DownloadDocumentScan(scanId);
        }

        [HttpPut]
        [Route("/documents/editDocument/{DocumentId}")]
        public async Task<ActionResult<Response>> EditDocument([FromRoute][Required] Guid? documentId)
        {
            return await _documentsService.EditDocument(documentId);
        }

        [HttpGet]
        [Route("/documents/getEducationDocument")]
        public async Task<ActionResult<Response>> GetEducationDocument()
        {
            return await _documentsService.GetEducationDocument();
        }


        [HttpGet]
        [Route("/documents/{ApplicantId}/getEducationDocument")]
        public async Task<ActionResult<Response>> GetApplicantEducationDocument([FromRoute][Required] Guid? applicantId)
        {
            return await _documentsService.GetApplicantEducationDocument(applicantId);
        }


        [HttpGet]
        [Route("/documents/getPassport")]
        public async Task<ActionResult<Response>> GetPassport()
        {
            return await _documentsService.GetPassport();
        }


        [HttpGet]
        [Route("/documents/{ApplicantId}/getPassport")]
        public async Task<ActionResult<Response>> GetApplicantPassport([FromRoute][Required] Guid? applicantId)
        {
            return await _documentsService.GetApplicantPassport(applicantId);
        }

    }
}
