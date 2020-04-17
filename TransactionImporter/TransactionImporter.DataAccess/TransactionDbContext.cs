using DummyMvc.DataAccess.EntitiesConfigurations;
using Microsoft.EntityFrameworkCore;
using TransactionImporter.DataAccess.Entities;

namespace DummyMvc.DataAccess
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public TransactionDbContext(DbContextOptions options) : base(options)
           => Database.EnsureCreated();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfiguration(new TransactionEntityConfiguration());
    }
}