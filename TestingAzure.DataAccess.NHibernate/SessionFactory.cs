using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using TestingAzure.DataAccess.Interfaces;

namespace TestingAzure.DataAccess.NHibernate
{
    public static class SessionFactory
    {
        public static ISessionFactory CreateSessionFactory(IConnectionString connectionString)
        {
            return Fluently.Configure()
                        .Database(OracleClientConfiguration.Oracle10.ConnectionString(connectionString.ConnectionString)
                        .Driver<LoggerClientDriver>()
                        )
                        .Mappings(map => map.FluentMappings.AddFromAssemblyOf<StadiumMap>())
                        .BuildSessionFactory();
        }
    }
}
