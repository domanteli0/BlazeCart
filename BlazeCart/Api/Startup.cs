using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

using DB;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Api.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;


        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            //var builder =  ;

            // Add services to the container.

            //string connectionString = configuration
            //    //.GetConnectionString("ProcessedDB") ?? configuration["ProcessedDB"];
            //    .GetConnectionString("ProcessedDB");
            //// Same as, but VS seems to be borked so above will have to do.
            ////string connectionString =  configuration.GetConnectionStringOrSetting(connectionName);

            ////var config = builder.GetContext().Configuration;
            ////var a = config.GetSection("DB").Value;
            //var conStr = config.GetConnectionStringOrSetting(a);


            ////builder.Services.AddDbContext<ScraperDbContext>(
            ////    options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, conStr));
            ////DbContextOptionsBuilder<ScraperDbContext> optionsBuilder = new DbContextOptionsBuilder<ScraperDbContext>()
            ////    .UseSqlServer(connectionString);

            //builder.Services.AddDbContext<ScraperDbContext>(
            //    options => options.UseSqlServer(connectionString));


            services.AddControllers();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            //string connectionString = configuration.GetConnectionString(configuration.GetSection("DB").Value!);

            //string connectionString = configuration.GetConnectionString("ProcessedDB");
            // If no connection string is found (i.e. GetConnectionString("thisKeyHasNoCorrespondingValue"))
            // then System.ArgumentNullException will be thrown.
            // If you're hosting this as a Azure App Service
            // Then you vitit [your.api.domain.com]/ probably get `HTTP Error 500.30 - ASP.NET Core app failed to start` or just blank page white page (404)
            // 
            // HAVE FUN FIGURING OUT WHY NO CONNSTR IS READ FROM THE ENVIRONMNET
            // EVEN THOUGH YOU HAVE SPECIFIED IT
            //var conName = "ProcessedDB";
            var conName = Configuration.GetConnectionString("DB") ?? Configuration["DB"];
            services.AddDbContext<ScraperDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(conName) ?? Configuration[conName]));

            //services.AddDbContext<ScraperDbContext>(
            //    options => options.UseSqlServer(conName));


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //builder.Services.BuildServiceProvider().GetService<ScraperDbContext>()!.Database.Migrate();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //var app = builder.Build();

            //Console.WriteLine(app.Configuration.GetSection("DB").Value);

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            //app.MapControllers();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

