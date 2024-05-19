using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.BL.Consumers
{
    public class ProgramExistConsumer: IConsumer<GetProgramExistBoolRequest>
    {
        private readonly IDictionaryRequestService _requestService;

        public ProgramExistConsumer(IDictionaryRequestService requestService)
        {
            _requestService = requestService;
        }

        public async Task Consume(ConsumeContext<GetProgramExistBoolRequest> context)
        {
            ProgramExistBool programExist = new ProgramExistBool();
            programExist.Exist = await _requestService.IsUniversityProgramExist(context.Message.ProgramId);

            await context.RespondAsync(programExist);
        }

    }
}
