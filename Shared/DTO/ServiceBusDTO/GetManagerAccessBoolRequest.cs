namespace Shared.DTO.ServiceBusDTO
{
    public class GetManagerAccessBoolRequest
    {
        public Guid ApplicantId {  get; set; }
        public Guid ManagerId {  get; set; }
    }
}
