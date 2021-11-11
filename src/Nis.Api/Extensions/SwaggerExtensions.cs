﻿using System.IO;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nis.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerServices(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "NIS API",
                        Contact = new OpenApiContact
                        {
                            Name = configuration["Contacts:Vydra:Name"],
                            Email = configuration["Contacts:Vydra:Email"]
                        }
                    });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "Nis.Api.xml");
                options.IncludeXmlComments(filePath);
            });

        
        public static void UseSwaggerDocumentation(this IApplicationBuilder app) => app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "NIS API v1");
                options.InjectStylesheet("/swagger/ui/theme-material.css");
            });
    }
}
