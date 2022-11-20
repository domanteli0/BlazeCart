using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DB
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ScraperDbContext>
    {
        private string _conStr;
        public DbContextFactory() : base()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<DbContextFactory>()
                .Build();

            _conStr = configuration.GetConnectionString("ScraperDB");
        }

        public DbContextFactory(string conString) : base()
        {
            _conStr = conString;
        }

        public ScraperDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ScraperDbContext> optionsBuilder = new DbContextOptionsBuilder<ScraperDbContext>()
                .UseSqlServer(_conStr);

            return new ScraperDbContext(optionsBuilder.Options);
        }
    }
}

