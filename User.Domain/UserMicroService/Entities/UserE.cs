using Microsoft.AspNetCore.Identity;
using Shared.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace User.Domain.Entities
{
    public class UserE : IdentityUser
    {
        // Общие поля для всех пользователей
        [Key]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleEnum Role { get; set; } = RoleEnum.Applicant;

        // Поля для абитуриентов
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? Citizenship { get; set; }
        public string? PhoneNumber { get; set; }

        // Поля для менеджеров
        public int? FacultyId { get; set; } // Связь с факультетом
    }
}
