using Microsoft.OpenApi.Models;
using Microsoft.Net.Http.Headers;
using Nis.Api.Authentication.Moodle;

namespace Nis.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(MoodleDefaults.AuthenticationScheme, new()
            {
                In = ParameterLocation.Header,
                Name = HeaderNames.Authorization,
                Type = SecuritySchemeType.ApiKey,
                Scheme = MoodleDefaults.AuthenticationScheme
            });
            options.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = MoodleDefaults.AuthenticationScheme
                        }
                    },
                    []
                }
            });
            options.SwaggerDoc(
                "v1",
                new()
                {
                    Title = "NIS API",
                    Contact = new()
                    {
                        Name = configuration["Contacts:Vydra:Name"] ?? "Jakub Vydra",
                        Email = configuration["Contacts:Vydra:Email"] ?? "jakubvydra.sw@gmail.com"
                    }
                });
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Nis.Api.xml"));
        });

        return services;
    }

    public static void UseSwaggerDocumentation(this IApplicationBuilder app) => app
        .UseSwagger()
        .UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "NIS API v1");
            options.InjectStylesheet("https://cdn.jsdelivr.net/npm/swagger-ui-themes@3.0.0/themes/3.x/theme-material.css");
        });
}
