using MassTransit;
using Shared.DTO;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;
using Shared.Models.Enums;
using User.Domain.Context;

public class GetUserConsumer : IConsumer<GetUserDTO>
{
    public async Task Consume(ConsumeContext<GetUserDTO> context)
    {
        // Получение информации о пользователе в зависимости от Email или UserId
        var userRights = new UserRights();
        if (context.Message.Email != null)
        {
            var email = context.Message.Email;
            userRights = new UserRights { Id = Guid.NewGuid(), Roles = new List<RoleEnum>() };
        }
        else
        {
            var id = context.Message.UserId;
            userRights = new UserRights { Id = Guid.NewGuid(), Roles = new List<RoleEnum>() };
        }

        await context.RespondAsync(userRights);
    }
}
