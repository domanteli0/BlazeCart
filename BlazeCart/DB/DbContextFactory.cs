using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DB
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ScraperDbContext>
    {
        private string _conStrKey;
        public DbContextFactory(string conStringKey) : base()
        {
            _conStrKey = conStringKey;
        }

        public ScraperDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddUserSecrets<Program>()
              .Build();
            
            string connectionString = configuration.GetConnectionString(_conStrKey)!;

            DbContextOptionsBuilder<ScraperDbContext> optionsBuilder = new DbContextOptionsBuilder<ScraperDbContext>()
                .UseSqlServer(connectionString);

            return new ScraperDbContext(optionsBuilder.Options);
        }
    }
}

