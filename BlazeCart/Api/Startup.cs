using DB;
using Microsoft.EntityFrameworkCore;
using Api.Repositories;
using Api.Middleware;
using Api.Aspects;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;

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
        public ILifetimeScope AutofacContainer { get; private set; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

          
            var conName = Configuration.GetConnectionString("DB") ?? Configuration["DB"];
            services.AddDbContext<ScraperDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString(conName) ?? Configuration[conName]));

            services.AddLogging();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ItemRepository>().As<IItemRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>()
                .EnableInterfaceInterceptors().InterceptedBy(typeof(LogAspect))
                .InstancePerDependency();

            builder.RegisterType<LogAspect>().SingleInstance();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

