using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TestingAzure.DataAccess.Interfaces;
using TestingAzure.DataAccess.EntityFramework;

namespace TestingAzure.WebApiNetCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Net Core API", Version = "v1" });
            });

            //services.AddTransient<DbContext>();
            //services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IUnitOfWork, DataAccess.NHibernate.UnitOfWork>();

            var conn = new ConnectionString(Configuration.GetConnectionString("DbConnection"));
            services.AddSingleton<IConnectionString>(conn);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Net Core API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
