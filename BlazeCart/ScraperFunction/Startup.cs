using System.IO;
using DB;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ScraperFunction
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var conf = builder.GetContext().Configuration;
            var conStr = conf.GetConnectionString("ScraperDB");

            if (conStr is null)
                throw new Exception("ScraperDB not found");

            builder.Services.AddDbContext<ScraperDbContext>(
                options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, conStr));
        }
    }
}

