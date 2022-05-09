using Microsoft.Extensions.Configuration;

namespace Nis.WpfApp;

public partial class App
{
    public static IConfiguration Configuration { get; private set; }

    public App() => Configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
        .Build();
}
