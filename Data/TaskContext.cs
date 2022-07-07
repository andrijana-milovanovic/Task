using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace Data
{
    public class TaskContext : DbContext
    {
        private IConfiguration _configuration;
        public TaskContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentDetails> DocumentsDetails { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var databaseConnectionString = _configuration.GetConnectionString("DatabaseConnectionString");

            optionsBuilder.UseSqlServer(@databaseConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Document>()
                        .HasMany(document => document.DocumentsDetails)
                        .WithOne(documentDetails => documentDetails.Document);
            

            modelBuilder.Entity<DocumentDetails>().HasKey(documentDetails => new
            {
                documentDetails.Id,
                documentDetails.DocumentId
            });

        }
        
    }
}
