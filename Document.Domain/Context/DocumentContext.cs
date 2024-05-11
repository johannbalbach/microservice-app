
using Document.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Document.Domain.Context
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        {

        }

        public DbSet<EducationDocument> EducationDocuments { get; set; }
        public DbSet<Passport> Passports { get; set; }
        public DbSet<FileDocument> fileDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
