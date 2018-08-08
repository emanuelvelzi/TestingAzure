using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;
using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private ITransaction _transaction;

        public ISession Session { get; set; }


        public UnitOfWork(IConnectionString connectionString)
        {
            if (_sessionFactory == null)
            {
                //_sessionFactory = Fluently.Configure()
                //           .Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionString.ConnectionString)
                //           .Driver<LoggerClientDriver>()
                //           )
                //           .Mappings(map => map.FluentMappings.AddFromAssemblyOf<StadiumMap>())
                //           .BuildSessionFactory();

                /*
                _sessionFactory = Fluently.Configure()
                           .Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionString.ConnectionString)
                           .Driver<OracleDataClientDriver>()
                           )
                           .Mappings(map => map.FluentMappings.AddFromAssemblyOf<StadiumMap>())
                           .BuildSessionFactory();

                _sessionFactory = Fluently.Configure()
                          .Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionString.ConnectionString)
                          .Driver<OracleLiteDataClientDriver>()
                          )
                          .Mappings(map => map.FluentMappings.AddFromAssemblyOf<StadiumMap>())
                          .BuildSessionFactory();
*/
                _sessionFactory = Fluently.Configure()
                         .Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionString.ConnectionString)
                         .Driver<LoggerClientDriver>()
                         )
                         .Mappings(map => map.FluentMappings.AddFromAssemblyOf<StadiumMap>())
                         .BuildSessionFactory();
                
            }

            Session = _sessionFactory.OpenSession();
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
