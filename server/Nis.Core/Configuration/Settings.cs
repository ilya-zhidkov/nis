using Microsoft.Extensions.Configuration;

namespace Nis.Core.Configuration;

public static class Settings
{
    public static IConfiguration Configuration => new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
        .Build();
}
