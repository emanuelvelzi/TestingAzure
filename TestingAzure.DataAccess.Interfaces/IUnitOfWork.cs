using System;

namespace TestingAzure.DataAccess.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();

        IRepository<T> GetRepository<T>() where T : class;
    }
}
