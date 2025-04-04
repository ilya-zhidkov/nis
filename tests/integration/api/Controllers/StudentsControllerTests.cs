using System.Net;
using Nis.Core.Configuration;
using Microsoft.Net.Http.Headers;

namespace Nis.Api.IntegrationTests.Controllers;

public class StudentsControllerTests : BaseIntegrationTest
{
    [Fact]
    public async Task it_should_get_all_students()
    {
        var response = await Http.SendAsync(new(HttpMethod.Get, $"{Http.BaseAddress}/students")
        {
            Headers =
            {
                {
                    HeaderNames.Authorization,
                    await GetTokenAsync(Settings.Configuration["Moodle:Credentials:Username"]!, Settings.Configuration["Moodle:Credentials:Password"]!)
                }
            }
        });

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(await response.Content.ReadFromJsonAsync<IEnumerable<IDictionary<string, object>>>());
    }

    [Theory]
    [InlineData("students")]
    [InlineData("students/username")]
    public async Task it_should_return_unauthorized_if_authorization_header_was_not_supplied(string segment) => Assert.Equal(
        HttpStatusCode.Unauthorized,
        (await Http.SendAsync(request: new(HttpMethod.Get, $"{Http.BaseAddress}/{segment}"))).StatusCode
    );
}
