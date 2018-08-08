using Microsoft.EntityFrameworkCore;
using TestingAzure.DataAccess.Interfaces;
using TestingAzure.Entities;

namespace TestingAzure.DataAccess.EntityFramework
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        private IConnectionString connectionString;

        public DbContext(IConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString.ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Entity<Stadium>().ToTable(nameof(Stadium));
        }

        public DbSet<Stadium> Stadiums { get; set; }
    }
}
