using System.Web.Http;
using TestingAzure.DataAccess.EntityFramework;
using TestingAzure.DataAccess.Interfaces;
using Unity;
using Unity.WebApi;

namespace TestingAzure.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IUnitOfWork, UnitOfWork>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}