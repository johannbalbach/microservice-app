using MassTransit;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace User.BL.Consumers
{
    public class DocumentRequestConsumer : IConsumer<DocumentRequestMessage>
    {
        private readonly IUserRequestsService _userService;

        public DocumentRequestConsumer(IUserRequestsService userService)
        {
            _userService = userService;
        }

        public async Task Consume(ConsumeContext<DocumentRequestMessage> context)
        {
            var message = context.Message;

            if (message.ApplicantId.HasValue)
            {
                // Обработка запроса по ApplicantId
                await _userService.HandleDocumentRequest(message.ApplicantId.Value, message.DocumentGuid);
            }
            else if (!string.IsNullOrEmpty(message.ApplicantEmail))
            {
                // Обработка запроса по ApplicantEmail
                await _userService.HandleDocumentRequest(message.ApplicantEmail, message.DocumentGuid);
            }
        }
    }
}
