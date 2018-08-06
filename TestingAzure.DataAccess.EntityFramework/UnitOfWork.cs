using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;
        public UnitOfWork()
        {
            this.dbContext = new DbContext();
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
            this.dbContext = new DbContext();
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
