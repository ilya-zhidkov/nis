using Xunit;
using Bogus.DataSets;
using Caliburn.Micro;
using FluentAssertions;
using Nis.WpfApp.Requests;
using Nis.Core.Configuration;

namespace Nis.WpfApp.IntegrationTests.Requests;

public class SignInRequestTests : BaseIntegrationTest
{
    private readonly SignInRequest _request;

    public SignInRequestTests() => _request = new SignInRequest(new SimpleContainer());

    [Fact]
    public async Task it_should_return_authentication_response_if_credentials_are_valid() => (await _request
        .SignInAsync(Settings.Configuration["Credentials:Username"], Settings.Configuration["Credentials:Password"]))
        .Should().BeOfType<SignInRequest.AuthenticationResponse>();

    [Fact]
    public async Task it_should_return_null_if_credentials_are_not_valid() => (await _request
        .SignInAsync(new Internet().UserName(), new Internet().Password()))
        .Should().BeNull();
}
