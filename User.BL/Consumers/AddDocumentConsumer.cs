using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace User.BL.Consumers
{
    public class AddDocumentConsumer: IConsumer<AddAttributeToUserRequest>
    {
        private readonly IUserRequestsService _userService;
        public AddDocumentConsumer(IUserRequestsService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<AddAttributeToUserRequest> context)
        {
            await _userService.AddDocumentToUser(context.Message);
        }
    }
}
