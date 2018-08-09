using NHibernate;
using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory SessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; set; }


        public UnitOfWork(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
            Session = SessionFactory.OpenSession();
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
        }

        public void Rollback()
        {
            if (_transaction != null && _transaction.IsActive)
                _transaction.Rollback();
        }


        public void Dispose()
        {
            Session.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(this.Session);
        }
    }
}
