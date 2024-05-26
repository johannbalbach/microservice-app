using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Dictionary.BL.Consumers
{
    public class EntityExistConsumer: IConsumer<GetDictionaryEntityExistBoolRequest>
    {
        private readonly IDictionaryRequestService _requestService;

        public EntityExistConsumer(IDictionaryRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Consume(ConsumeContext<GetDictionaryEntityExistBoolRequest> context)
        {
            DictionaryEntityExistBool programExist = new DictionaryEntityExistBool();
            programExist.Exist = await _requestService.IsDictionaryEntityExist(context.Message);

            await context.RespondAsync(programExist);
        }

    }
}
