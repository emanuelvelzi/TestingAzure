using CommonServiceLocator;
using System.Configuration;
using System.Web.Http;
using TestingAzure.DataAccess.EntityFramework;
using TestingAzure.DataAccess.Interfaces;
using Unity;
using Unity.ServiceLocation;
using Unity.WebApi;

namespace TestingAzure.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            var locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);

            var conn = new ConnectionString(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString);
            container.RegisterInstance<IConnectionString>(conn);

            if (ConfigurationManager.AppSettings["Database"] == "Oracle")
            {
                container.RegisterType<IUnitOfWork, DataAccess.NHibernate.UnitOfWork>();
            }
            else // Azure / SqlServer
            {
                container.RegisterType<IUnitOfWork, UnitOfWork>();
                container.RegisterType<DbContext>();
            }

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}