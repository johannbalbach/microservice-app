using Microsoft.AspNetCore.Identity;
using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace User.Domain.Entities
{
    public class UserE : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RoleEnum> Roles { get; set; } = new List<RoleEnum>();
        public Applicant Applicant { get; set; } = null!;
        public Manager Manager { get; set; } = null!;
    }
}
