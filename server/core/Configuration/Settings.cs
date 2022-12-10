using Microsoft.Extensions.Configuration;

namespace Nis.Core.Configuration;

public static class Settings
{
    private static readonly ConfigurationBuilder Builder = new();

    public static IConfiguration Configuration
    {
        get
        {
            Builder.AddJsonFile("appsettings.json");

            #if !RELEASE
                Builder.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
            #else
                Builder.AddJsonFile("appsettings.Production.json");
            #endif

            return Builder.Build();
        }
    }
}
