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
using Api.Services;

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
            services.AddControllers();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAlgorithmService, AlgorithmService>();

            // If no connection string is found (i.e. GetConnectionString("thisKeyHasNoCorrespondingValue"))
            // then System.ArgumentNullException will be thrown.
            // If you're hosting this as a Azure App Service
            // Then you vitit [your.api.domain.com]/ you'll probably get `HTTP Error 500.30 - ASP.NET Core app failed to start` or just blank page white page (404)
            // 
            // HAVE FUN FIGURING OUT WHY NO CONNSTR IS NOT BEING READ FROM THE ENVIRONMNET
            // EVEN THOUGH YOU HAVE SPECIFIED IT
            var conName = Configuration.GetConnectionString("DB") ?? Configuration["DB"];
            services.AddDbContext<ScraperDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(conName) ?? Configuration[conName]));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            //app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

