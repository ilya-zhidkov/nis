using Nis.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Nis.Api
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Seed().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}
