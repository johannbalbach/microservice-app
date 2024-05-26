using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Dictionary.BL.Consumers
{
    public class ProgramDocumentsMatchesConsumer : IConsumer<GetProgramsAndDocumentsMatches>
    {
        private readonly IDictionaryRequestService _requestService;

        public ProgramDocumentsMatchesConsumer(IDictionaryRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Consume(ConsumeContext<GetProgramsAndDocumentsMatches> context)
        {
            ProgramDocumentsMatches programExist = new ProgramDocumentsMatches();
            programExist.MatchesDocumentsId = await _requestService.MatchesDocumentTypesForPrograms(context.Message.ProgramsIds);

            await context.RespondAsync(programExist);
        }

    }
}
