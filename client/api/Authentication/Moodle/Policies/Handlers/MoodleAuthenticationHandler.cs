using System.Net.Mime;
using System.Text.Json;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;

namespace Nis.Api.Authentication.Moodle.Policies.Handlers;

internal class MoodleAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder
) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    // Use snake case for consistency with Moodle's API.
    private new static readonly JsonSerializerOptions Options = new() { PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower }; 

    protected override Task<AuthenticateResult> HandleAuthenticateAsync() => Task.FromResult(AuthenticateResult.NoResult());

    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
        Response.StatusCode = StatusCodes.Status401Unauthorized;
        Response.ContentType = MediaTypeNames.Application.Json;

        await Response.WriteAsync(JsonSerializer.Serialize(new
        {
            error = "invalid_token",
            message = "The access token is missing, expired, or invalid."
        }, Options));
    }
}
