﻿using Microsoft.AspNetCore.Mvc;
using Shared.DTO;
using Shared.Interfaces;
using Shared.Models;

namespace Document.BL.Services
{
    public class DocumentsService : IDocumentService
    {
        public async Task<ActionResult<Response>> AddApplicantEducationDocument(DocumentCreateDTO body)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> AddApplicantPassport(PassportCreateDTO body)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> DeleteDocumentScan(Guid? scanId)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> DownloadDocumentScan(Guid? scanId)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> EditDocument(Guid? documentId)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> GetEducationDocument()
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> GetApplicantEducationDocument(Guid? applicantId)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> GetPassport()
        {
            // Implement your logic here
            throw new NotImplementedException();
        }

        public async Task<ActionResult<Response>> GetApplicantPassport(Guid? applicantId)
        {
            // Implement your logic here
            throw new NotImplementedException();
        }
    }
}
