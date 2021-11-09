using System;
using System.IO;
using Nis.Core.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Nis.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<RouteOptions>(options => options.LowercaseUrls = true)
                .ConnectToDatabase($"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nis_development.db")}");

            services.AddControllers();

            services
                .AddSwaggerGen(options => options
                    .SwaggerDoc("v1", new OpenApiInfo { Title = "Nis.Api", Version = "v1" }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app
                    .UseDeveloperExceptionPage()
                    .UseSwagger()
                    .UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "Nis.Api v1"));

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
