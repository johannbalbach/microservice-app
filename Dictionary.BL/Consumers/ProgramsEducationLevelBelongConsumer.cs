using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Dictionary.BL.Consumers
{
    public class ProgramsEducationLevelBelongConsumer : IConsumer<GetProgramsEducationLevelsBelong>
    {
        private readonly IDictionaryRequestService _requestService;

        public ProgramsEducationLevelBelongConsumer(IDictionaryRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Consume(ConsumeContext<GetProgramsEducationLevelsBelong> context)
        {
            ProgramsEducationLevelsBool response = new ProgramsEducationLevelsBool();
            response.Value = await _requestService.IsProgramsEducationLevelsSame(context.Message.ProgramsId);

            await context.RespondAsync(response);
        }

    }
}