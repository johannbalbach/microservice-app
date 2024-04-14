using Microsoft.AspNetCore.Identity;
using Shared.Models.Enums;

namespace User.Domain.Entities
{
    public class UserE : IdentityUser
    {
        // Общие поля для всех пользователей
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // Поля для абитуриентов
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public string Citizenship { get; set; }
        public string PhoneNumber { get; set; }

        // Поля для менеджеров
        public int? FacultyId { get; set; } // Связь с факультетом
    }
}
