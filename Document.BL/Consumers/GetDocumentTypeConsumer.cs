using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Document.BL.Consumers
{
    public class GetDocumentTypeConsumer: IConsumer<GetEducationDocumentType>
    {
        private readonly IDocumentRequestService _requestService;

        public GetDocumentTypeConsumer(IDocumentRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Consume(ConsumeContext<GetEducationDocumentType> context)
        {
            EducationDocumentType response = new EducationDocumentType();
            response = await _requestService.GetApplicantEducationDocument(context.Message.ApplicantId);

            await context.RespondAsync(response);
        }
    }
}
