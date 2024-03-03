using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Accounting;
using Accounting.Infrastructure.DataAccess;
using Serilog;
using Autofac.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

Host.CreateDefaultBuilder(args)
    .UseContentRoot(Directory.GetCurrentDirectory())
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .UseSerilog((hostContext, services, config) =>
    {
        config.MinimumLevel.Information();
        config.WriteTo.Console();
    })
    .ConfigureLogging(x => x.AddSerilog())
    .ConfigureWebHostDefaults(config =>
    {
        config.UseStartup<Startup>();
    })
    .Build()
    .Run();
