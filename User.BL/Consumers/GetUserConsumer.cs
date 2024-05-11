using MassTransit;
using Shared.DTO;
using Shared.DTO.ServiceBusDTO;
using Shared.Interfaces;

public class GetUserConsumer : IConsumer<GetUserDTORequest>
{
    private readonly IUserRequestsService _userService;
    public GetUserConsumer(IUserRequestsService userService)
    {
        _userService = userService;
    }

    public async Task Consume(ConsumeContext<GetUserDTORequest> context)
    {
        // Получение информации о пользователе в зависимости от Email или UserId
        var userRights = new UserRights();
        if (context.Message.Email != null)
            userRights = await _userService.GetUserRights(context.Message.Email);
        else if (context.Message.UserId != null)
            userRights = await _userService.GetUserRights((Guid)context.Message.UserId);

        await context.RespondAsync(userRights);
    }
}
