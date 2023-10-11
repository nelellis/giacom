using DataHandler.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataHandler.CdrDbContext
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<CallDetailRecord> CallDetailRecords { get; set; }
    }
}