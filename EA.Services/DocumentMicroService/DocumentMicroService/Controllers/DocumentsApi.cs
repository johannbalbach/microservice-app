using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Document.Domain.Models;

namespace Document.Domain.Controllers
{
    [ApiController]
    public class DocumentsApiController : ControllerBase
    {
        [HttpPost]
        [Route("/documents/addEducationDocument")]
        public async Task<ActionResult<Response>> AddApplicantEducationDocument([FromBody] DocumentCreateDTO body)
        {

        }

        [HttpPost]
        [Route("/documents/addPassport")]
        public async Task<ActionResult<Response>> AddApplicantPassport([FromBody] PassportCreateDTO body)
        {

        }

        [HttpDelete]
        [Route("/documents/deleteScan/{ScanId}")]
        public async Task<ActionResult<Response>> DeleteDocumentScan([FromRoute][Required] Guid? scanId)
        {

        }

        [HttpGet]
        [Route("/documents/download/{ScanId}")]
        public async Task<ActionResult<Response>> DownloadDocumentScan([FromRoute][Required] Guid? scanId)
        {

        }

        [HttpPut]
        [Route("/documents/editDocument/{DocumentId}")]
        public async Task<ActionResult<Response>> EditDocument([FromRoute][Required] Guid? documentId)
        {

        }

        [HttpGet]
        [Route("/documents/getEducationDocument")]
        public async Task<ActionResult<Response>> GetEducationDocument()
        {

        }


        [HttpGet]
        [Route("/documents/{ApplicantId}/getEducationDocument")]
        public async Task<ActionResult<Response>> GetApplicantEducationDocument([FromRoute][Required] Guid? applicantId)
        {

        }


        [HttpGet]
        [Route("/documents/getPassport")]
        public async Task<ActionResult<Response>> GetPassport()
        {

        }


        [HttpGet]
        [Route("/documents/{ApplicantId}/getPassport")]
        public async Task<ActionResult<Response>> GetApplicantPassport([FromRoute][Required] Guid? applicantId)
        {

        }


    }
}
