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
        public DbSet<EducationLevel> EducationLevels { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DocumentType>()
                .HasMany(d => d.NextEducationLevels)
                .WithMany(e => e.DocumentTypes);
            modelBuilder.Entity<DocumentType>()
                .HasOne(d => d.EducationLevel);

            modelBuilder.Entity<EducationLevel>()
                .HasMany(d => d.DocumentTypes)
                .WithMany(e => e.NextEducationLevels);
        }
    }
}
