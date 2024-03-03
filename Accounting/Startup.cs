using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Accounting.Application.Services;
using Accounting.Extensions;
using Accounting.Infrastructure.DataAccess;
using Accounting.Models;
using Accounting.Application.Queries;
using Autofac;
using System.Reflection;


namespace Accounting
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddAutoMapper(GetType());
            
            string mySqlConnectionStr = _configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<DataContext>(options =>
                {
                    options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr));
                });


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            services.AddCors();
            //services.AddScoped<DbInitializer>();
            //services.AddScoped<ValueServices>();
            //services.AddScoped<ItemServices>();
            //services.AddScoped<ItemQueries>();

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            //Assembly.GetExecutingAssembly().GetTypes()
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(t => t.Name.EndsWith("Services") || t.Name.EndsWith("Queries"))
                .ToArray();

            builder.RegisterTypes(types)
                .InstancePerLifetimeScope()
                .AsSelf();

            builder.RegisterType<DbInitializer>().AsSelf();
            
        }
        
        public void Configure(IApplicationBuilder app)
        {

            app.UseCors(policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed(_ => true);
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                dbInitializer.DbInitialize(dbContext);

            }

            app.UseStaticFiles();

            app.UseRouting();

            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers().RequireAuthorization();
                endpoints.MapControllers();
                // endpoints.MapControllerRoute(
                //     name: "fallback",
                //     pattern: "/{app}/{**any}",
                //     defaults: new { controller = "Apps", action = "Index" });
            });
        }
    }
}
