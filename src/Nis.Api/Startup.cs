using System;
using System.IO;
using Nis.Api.Extensions;
using Nis.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nis.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .Configure<RouteOptions>(options => options.LowercaseUrls = true)
                .ConnectToDatabase(
                    $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "nis_development.db")}");

            services.AddControllers();

            services
                .AddSwaggerServices(_configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app
                    .UseStaticFiles()
                    .UseDeveloperExceptionPage()
                    .UseSwaggerDocumentation();

            app
                .UseHttpsRedirection()
                .UseRouting()
                .UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
