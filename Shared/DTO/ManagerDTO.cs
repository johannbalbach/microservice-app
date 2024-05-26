using Shared.Models.Enums;

namespace Shared.DTO
{
    public class ManagerDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<RoleEnum> Roles { get; set; }
        public Guid FacultyId { get; set; }
    }
}
