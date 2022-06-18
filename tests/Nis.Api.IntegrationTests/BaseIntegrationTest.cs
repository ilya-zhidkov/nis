using Xunit;
using System.Text;
using System.Net.Mime;
using System.Text.Json;
using Nis.Core.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Nis.Api.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<BaseIntegrationTest.Fixture>
{
    protected HttpClient Http { get; }

    protected BaseIntegrationTest()
    {
        var factory = new WebApplicationFactory<Program>();

        Http = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false,
            BaseAddress = new Uri(Settings.Configuration["Api:Endpoint"])
        });
    }

    protected async Task<string> GetTokenAsync(string username, string password)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Http.BaseAddress}/auth/login")
        {
            Content = new StringContent(JsonSerializer.Serialize(new { username, password }), Encoding.UTF8, MediaTypeNames.Application.Json)
        };

        var response = await Http.SendAsync(request);

        return (await response.Content.ReadFromJsonAsync<IDictionary<string, string>>())?["token"];
    }

    private class Fixture : WebApplicationFactory<Program>
    {
        private readonly KeyValuePair<string, string> _environment = new("DOTNET_ENVIRONMENT", "Test");

        public Fixture() => Environment.SetEnvironmentVariable(_environment.Key, _environment.Value);

        protected override void Dispose(bool disposing)
        {
            if (Environment.GetEnvironmentVariable(_environment.Key) is not null)
                Environment.SetEnvironmentVariable(_environment.Key, null);

            base.Dispose(disposing);
        }
    }
}
