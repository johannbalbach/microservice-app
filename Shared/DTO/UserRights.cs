using Shared.Models.Enums;

namespace Shared.DTO
{
    public class UserRights
    {
        public Guid Id { get; set; }
        public List<RoleEnum> Roles { get; set; } = new List<RoleEnum>();
    }
}
