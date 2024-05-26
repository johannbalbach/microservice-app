using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Dictionary.BL.Consumers
{
    public class DocTypeEducationLevelBelongsConsumer : IConsumer<GetDocTypeEducationLevelBelongs>
    {
        private readonly IDictionaryRequestService _requestService;

        public DocTypeEducationLevelBelongsConsumer(IDictionaryRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Consume(ConsumeContext<GetDocTypeEducationLevelBelongs> context)
        {
            DocTypeEducationLevelBelongs programExist = new DocTypeEducationLevelBelongs();
            programExist.IsBelongs = await _requestService.IsDocumentTypeSameToProgam(context.Message.ProgramId, context.Message.DocumentTypeIds);

            await context.RespondAsync(programExist);
        }

    }
}