using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Ninja.Application.Common.Interfaces;
using Ninja.Application.Middlewares;
using Ninja.Application.Services;
using Ninja.Application.Users.Queries;
using System.IO;

namespace Ninja.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment _hostingEnvironment)
        {
            Configuration = configuration;
            hostingEnvironment = _hostingEnvironment;
        }

        private readonly IWebHostEnvironment hostingEnvironment;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IUserServiceRepository, UserService>();
            services.AddSingleton<ILoggin, Loggin>();

            services.AddMediatR(typeof(GetAllUsersQuery));

            services.AddSwaggerGen(
                config =>
                config.SwaggerDoc(
                    "NinjaAPI",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Controllers Info"
                    })
                );

            string basePath = hostingEnvironment.ContentRootPath;

            services.AddSwaggerGen(
                config =>
                config.IncludeXmlComments(Path.Combine(basePath, "Ninja.Api.xml"))
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Swagger Config
            app.UseSwagger();
            app.UseSwaggerUI(
                config =>
                config.SwaggerEndpoint("/swagger/NinjaAPI/swagger.json", "API de control de Usuarios")
                );
        }
    }
}
