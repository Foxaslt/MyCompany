using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyCompany.ProductCatalog.Api.Controllers;
using MyCompany.ProductCatalog.Api.Database;
using Swashbuckle.AspNetCore.Swagger;

namespace MyCompany.ProductCatalog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My Company API", Version = "v1" });
            });

            services.AddAntiforgery();

            //services.AddSingleton(new LoggerFactory()
            //    .AddConsole()
            //    .AddDebug());
            services.AddLogging(conf => conf
                    .AddConsole()
                    .AddDebug())
                .AddTransient<ProductsController>();

            var connectionString = Configuration.GetValue<string>("ConnectionString");
            //services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));
            //services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionString));
            services.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase("ProductDatabase"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Company API V1");
            });

            app.UseMvc();
        }
    }
}
