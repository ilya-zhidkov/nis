﻿namespace Nis.WpfApp.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<BaseIntegrationTest.Fixture>
{
    private class Fixture : IDisposable
    {
        private readonly KeyValuePair<string, string> _environment = new("DOTNET_ENVIRONMENT", "Test");

        public Fixture() => Environment.SetEnvironmentVariable(_environment.Key, _environment.Value);

        public void Dispose()
        {
            if (Environment.GetEnvironmentVariable(_environment.Key) is not null)
                Environment.SetEnvironmentVariable(_environment.Key, null);
        }
    }
}
