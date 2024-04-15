using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public class Token
    {
        [Key]
        public Guid UserId { get; set; }
        public UserE User { get; set; }
        public string RefreshToken { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public DateTime RefreshTokenExpiration { get; set; }
    }
}
