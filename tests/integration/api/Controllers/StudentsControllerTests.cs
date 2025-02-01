using System.Net;
using FluentAssertions;
using Nis.Core.Configuration;

namespace Nis.Api.IntegrationTests.Controllers;

public class StudentsControllerTests : BaseIntegrationTest
{
    [Fact]
    public async Task it_should_get_all_students()
    {
        var token = await GetTokenAsync(Settings.Configuration["Moodle:Credentials:Username"], Settings.Configuration["Moodle:Credentials:Password"]);
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Http.BaseAddress}/students") { Headers = { { "token", token } } };

        var response = await Http.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var students = await response.Content.ReadFromJsonAsync<IEnumerable<IDictionary<string, object>>>();
        students.Should().NotBeNull();
    }

    [Theory]
    [InlineData("students")]
    [InlineData("students/username")]
    public async Task it_should_return_unauthorized_if_authorization_header_was_not_supplied(string segment)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Http.BaseAddress}/{segment}");
        var response = await Http.SendAsync(request);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
