﻿using System;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DB;
using Scraper;
using CategoryMap.Implementations;
using Impl = CategoryMap.Implementations;
using CategoryMap;

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

            builder.Services.AddLogging();

            builder.Services.AddSingleton<IConfiguration>(config);
            builder.Services.AddScoped<HttpClient>(_ => new HttpClient());

            builder.Services.AddScoped<IDictionary<string, ICategoryMap>>(
                serv => new Dictionary<string, ICategoryMap>() {
                    { "BarboraScraper", new BarboraCategoryMap(
                        serv.GetService<ILogger<Impl.BarboraCategoryMap>>()
                        ) },
                    { "IKIScraper", new IkiCategoryMap(
                        serv.GetService<ILogger<Impl.BarboraCategoryMap>>()
                        ) },
                }
            );

            builder.Services.AddScoped<IScraper>(
                serv => new BarboraScraper(
                    serv.GetService<HttpClient>(),
                    serv.GetService<ILogger<Scraper.Scraper>>()
            ));

            builder.Services.AddScoped<IScraper>(
                serv => new IKIScraper(
                    serv.GetService<HttpClient>(),
                    serv.GetService<ILogger<Scraper.Scraper>>()
            ));
        }
    }
}

