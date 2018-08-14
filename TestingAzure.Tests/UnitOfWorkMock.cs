using System.Collections.Generic;
using System.Linq;
using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.Tests
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        private List<object> Repositories;
        public UnitOfWorkMock(params object[] repositories)
        {
            this.Repositories = repositories.ToList();
        }

        public void BeginTransaction()
        {
            return;
        }

        public void Commit()
        {
            return;
        }

        public void Dispose()
        {
            return;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return (IRepository<T>)this.Repositories.FirstOrDefault(x => (x as IRepository<T>) != null);
        }

        public void Rollback()
        {
            return;
        }
    }
}
