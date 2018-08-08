using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public void BeginTransaction()
        {

        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public void Rollback()
        {
           
        }


        public void Dispose()
        {
            this.dbContext?.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(this.dbContext.Set<T>());
        }
    }
}
