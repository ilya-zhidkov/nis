using Nis.Api.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace Nis.Api.Authentication.Moodle.Policies.Handlers;

[UsedImplicitly]
public class MoodleRequirementHandler(HttpClient http, IOptions<MoodleOptions> options) : AuthorizationHandler<RequireMoodleAccount>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RequireMoodleAccount requirement)
    {
        var request = (context.Resource as DefaultHttpContext)?.Request;

        if (request is null || !request.Headers.TryGetValue(HeaderNames.Authorization, out var token) || string.IsNullOrWhiteSpace(token))
        {
            context.Fail();
            return;
        }

        var (url, _, _, _, format) = options.Value;
        var response = await http.GetAsync($"{url}/webservice/rest/server.php?wstoken={token}&moodlewsrestformat={format}&wsfunction=core_webservice_get_site_info");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();

            // Despite the fact that the response is successful, it may contain an exception.
            if (content.Contains("exception"))
            {
                context.Fail();
                return;
            }
        }

        context.Succeed(requirement);
    }
}

public class RequireMoodleAccount : IAuthorizationRequirement;
