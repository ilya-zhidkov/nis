using Nis.WpfApp.Requests;
using Nis.Core.Configuration;

namespace Nis.WpfApp.IntegrationTests.Requests;

public sealed class SignInRequestTests : BaseIntegrationTest
{
    private readonly SignInRequest _request = new(container: new());

    [Fact]
    public async Task it_should_return_authentication_response_if_credentials_are_valid() =>
        Assert.IsType<SignInRequest.AuthenticationResponse>(await _request.SignInAsync(
            Settings.Configuration["Moodle:Credentials:Username"]!,
            Settings.Configuration["Moodle:Credentials:Password"]!)
        );

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", " ")]
    [InlineData(null, null)]
    public async Task it_should_return_null_if_credentials_are_not_valid(string? username, string? password) => Assert.Null(await _request.SignInAsync(username!, password!));
}
