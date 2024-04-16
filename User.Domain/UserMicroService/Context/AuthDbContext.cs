using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using User.Domain.Entities;

namespace User.Domain.Context
{
    public class AuthDbContext: IdentityDbContext<UserE, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>,
    IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, Token>
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserE>().HasOne(u => u.Applicant).WithOne(c => c.User).HasForeignKey<Applicant>();
            builder.Entity<UserE>().HasOne(u => u.Manager).WithOne(c => c.User).HasForeignKey<Manager>();
        }
    }
}
