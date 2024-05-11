namespace Shared.DTO.ServiceBusDTO
{
    public class GetUserDTORequest
    {
        public string? Email { get; set; }
        public Guid? UserId { get; set; }
    }
}
