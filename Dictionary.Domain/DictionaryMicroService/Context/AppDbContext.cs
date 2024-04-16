using Dictionary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Domain.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<UniversityProgram> Programs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
