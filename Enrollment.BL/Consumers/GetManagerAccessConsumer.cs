using MassTransit;
using Shared.DTO;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

namespace Enrollment.BL.Consumers
{
    public class GetManagerAccessConsumer: IConsumer<GetManagerAccessBoolRequest>
    {
        private readonly IEnrollmentRequestsService _enrollmentService;
        public GetManagerAccessConsumer(IEnrollmentRequestsService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }
        public async Task Consume(ConsumeContext<GetManagerAccessBoolRequest> context)
        {
            // Получение информации о пользователе в зависимости от Email или UserId
            bool ManagerAccess = await _enrollmentService.CheckManagerAssign(context.Message.ApplicantId, context.Message.ManagerId);

            await context.RespondAsync(ManagerAccess);
        }
    }
}
