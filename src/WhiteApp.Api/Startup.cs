using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using WhiteApp.Api.Data;

namespace WhiteApp.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(opt =>
              opt.UseInMemoryDatabase("InMemoryCustomerDB")
           );

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                 "OpenAPISpecification",
                 new Microsoft.OpenApi.Models.OpenApiInfo()
                 {
                     Title = "Customer API",
                     Version = "1",
                     Description = "Through this API you can access customer details",
                     Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                     {
                         Email = "amit.naik8103@gmail.com",
                         Name = "Amit Naik",
                         Url = new Uri("https://amitpnk.github.io/")
                     },
                     License = new Microsoft.OpenApi.Models.OpenApiLicense()
                     {
                         Name = "MIT License",
                         Url = new Uri("https://opensource.org/licenses/MIT")
                     }
                 });

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint
                ("/swagger/OpenAPISpecification/swagger.json", "Customer API");
                setupAction.RoutePrefix = "";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
