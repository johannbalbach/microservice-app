using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;

namespace User.Domain.Context
{
    public class AppDbContext: IdentityDbContext<UserE>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
