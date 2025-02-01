namespace Nis.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(options =>
        {
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
