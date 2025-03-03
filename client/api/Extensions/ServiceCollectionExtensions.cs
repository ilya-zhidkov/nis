using Nis.Core.Extensions;
using Nis.Core.Persistence;
using Nis.Api.Authentication.Moodle;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Nis.Api.Authentication.Moodle.Policies.Handlers;

namespace Nis.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options => options.UseSqlite(DatabaseExtensions.ConnectionString));

        return services;
    }

    public static AuthenticationBuilder AddMoodle(this AuthenticationBuilder builder)
    {
        builder.Services
            .AddSingleton<IAuthorizationHandler, MoodleRequirementHandler>()
            .AddAuthorizationBuilder()
            .AddPolicy(nameof(RequireMoodleAccount), policy => policy.Requirements.Add(new RequireMoodleAccount()));

        return builder.AddScheme<AuthenticationSchemeOptions, MoodleAuthenticationHandler>(MoodleDefaults.AuthenticationScheme, _ => { });
    }
}
