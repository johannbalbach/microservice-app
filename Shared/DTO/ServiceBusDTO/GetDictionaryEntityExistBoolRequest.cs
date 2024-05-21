using Shared.Enums;

namespace Shared.DTO.ServiceBusDTO
{
    public class GetDictionaryEntityExistBoolRequest
    {
        public Guid? EntityId { get; set; }
        public int? EntityIdInt { get; set; }
        public DictionaryEntities entityType{ get; set; }
    }
}
