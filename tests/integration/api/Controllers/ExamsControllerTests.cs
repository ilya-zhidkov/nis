using System.Net;
using System.Text;
using System.Net.Mime;
using Nis.Core.Configuration;
using Microsoft.Net.Http.Headers;

namespace Nis.Api.IntegrationTests.Controllers;

public sealed class ExamsControllerTests : BaseIntegrationTest
{
    [Fact]
    public async Task it_should_submit_an_exam()
    {
        var response = await Http.SendAsync(request: new(HttpMethod.Post, $"{Http.BaseAddress}/exams")
        {
            Headers =
            {
                {
                    HeaderNames.Authorization,
                    await GetTokenAsync(Settings.Configuration["Moodle:Credentials:Username"]!, Settings.Configuration["Moodle:Credentials:Password"]!)
                }
            },
            Content = new StringContent(await File.ReadAllTextAsync("Assets/Samples/Exam.json"), Encoding.UTF8, MediaTypeNames.Application.Json)
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);
        Assert.NotEmpty((await response.Content.ReadFromJsonAsync<IDictionary<string, string>>())!);
    }
}
