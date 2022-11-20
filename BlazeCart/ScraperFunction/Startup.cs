using System;
using DB;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

[assembly: FunctionsStartup(typeof(ScraperFunction.Startup))]
namespace ScraperFunction
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = builder.GetContext().Configuration;
            var a = config.GetSection("DB").Value;
            var conStr = config.GetConnectionStringOrSetting(a);

            if (conStr is null)
                throw new Exception("DB connection string not found");

            builder.Services.AddDbContext<ScraperDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, conStr));

            builder.Services.AddSingleton<IConfiguration>(config);
        }
    }
}

