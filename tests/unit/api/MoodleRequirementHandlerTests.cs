using System.Net;
using Nis.Api.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Nis.Api.Authentication.Moodle.Policies.Handlers;

namespace Nis.Api.UnitTests;

public sealed class MoodleRequirementHandlerTests
{
    private readonly IOptions<MoodleOptions> _options = Microsoft.Extensions.Options.Options.Create(new MoodleOptions { Url = "https://moodle.example.com" });

    [Fact]
    public async Task it_should_succeed_when_token_is_valid()
    {
        var handler = new MockHttpMessageHandler(response: _ => new(HttpStatusCode.OK));
        var requirement = new MoodleRequirementHandler(http: new(handler), _options);
        var authorization = Authorize(header: Guid.NewGuid().ToString());

        await requirement.HandleAsync(authorization);

        Assert.True(authorization.HasSucceeded);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task it_should_fail_when_token_is_invalid(string token)
    {
        var handler = new MockHttpMessageHandler(response: _ => new(HttpStatusCode.Unauthorized));
        var requirement = new MoodleRequirementHandler(http: new(handler), _options);
        var authorization = Authorize(header: token);

        await requirement.HandleAsync(authorization);

        Assert.True(authorization.HasFailed);
    }

    private static AuthorizationHandlerContext Authorize(string? header)
    {
        var requirement = new RequireMoodleAccount();

        var context = new AuthorizationHandlerContext(
            requirements: [requirement],
            resource: new DefaultHttpContext(),
            user: null!
        );

        if (header is not null)
            (context.Resource as DefaultHttpContext)!.Request.Headers.Authorization = header;

        return context;
    }
}

internal class MockHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> response) : HttpMessageHandler
{
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellation) => Task.FromResult(response(request));
}
