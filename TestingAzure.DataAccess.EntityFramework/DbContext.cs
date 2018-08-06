using Microsoft.EntityFrameworkCore;
using TestingAzure.Entities;

namespace TestingAzure.DataAccess.EntityFramework
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=tcp:azuresqldbsvr.database.windows.net,1433;Initial Catalog=AzureSqlDatabase;Persist Security Info=False;User ID=emanuelvelzi;Password=Claro$701234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
        protected override void OnModelCreating(ModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Entity<Stadium>().ToTable(nameof(Stadium));
        }

        public DbSet<Stadium> Stadiums { get; set; }
    }
}
