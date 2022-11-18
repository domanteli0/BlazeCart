using System;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DB;
using Microsoft.Extensions.Configuration;
using System.IO;

[assembly: FunctionsStartup(typeof(ScraperFunction.Startup))]
namespace ScraperFunction
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddUserSecrets<Startup>()
              .Build();

            string connectionString = configuration.GetConnectionString("AzureDB")!;

            DbContextOptionsBuilder<ScraperDbContext> optionsBuilder = new DbContextOptionsBuilder<ScraperDbContext>()
                .UseSqlServer(connectionString);

            builder.Services.AddDbContext<ScraperDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));
        }
    }
}

