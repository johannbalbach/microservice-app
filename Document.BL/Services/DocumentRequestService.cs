using Document.Domain.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Shared.DTO.ServiceBusDTO;
using Shared.Exceptions;
using Shared.Interfaces;

namespace Document.BL.Services
{
    public class DocumentRequestService: IDocumentRequestService
    {
        private readonly DocumentContext _context;

        public DocumentRequestService(DocumentContext context)
        {
            _context = context;
        }

        public async Task<EducationDocumentType> GetApplicantEducationDocument(Guid ApplicantId)
        {
            EducationDocumentType response = new EducationDocumentType();
            var educationDocument = await _context.EducationDocuments.Where(a => a.ApplicantId == ApplicantId).ToListAsync();

            if (educationDocument.Count == 0 || educationDocument == null)
            {
                response.IsSuccess = false;
                return response;
            }
            educationDocument.ForEach(d => response.DocumentTypeIds.Add(d.DocumentTypeGuid));
            response.IsSuccess = true;

            return response;    
        }
    }
}
