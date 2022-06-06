using Nis.Api.Options;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Nis.Api.Controllers;

public class CoursesController : BaseApiController
{
    private readonly HttpClient _client;
    private readonly MoodleOptions _options;

    public CoursesController(
        HttpClient client,
        IOptions<MoodleOptions> options
    )
    {
        _client = client;
        _options = options.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromHeader] string token)
    {
        var (url, _, _, _, format) = _options;

        var response = await _client.GetAsync($"{url}/webservice/rest/server.php?wstoken={token}&moodlewsrestformat={format}&wsfunction=core_course_get_courses");
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            return Ok(JsonSerializer.Deserialize<IEnumerable<IDictionary<string, object>>>(content));
        }
        catch (JsonException exception)
        {
            var error = JsonSerializer.Deserialize<IDictionary<string, string>>(content);

            return Unauthorized(new { message = error?["message"] ?? exception.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Show([FromHeader] string token, ushort id)
    {
        var (url, _, _, _, format) = _options;
        var response = await _client.GetAsync($"{url}/webservice/rest/server.php?wstoken={token}&moodlewsrestformat={format}&wsfunction=mod_assign_get_assignments&courseids[0]={id}");
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            return Ok(JsonSerializer.Deserialize<IDictionary<string, IEnumerable<IDictionary<string, object>>>>(content)?["courses"].FirstOrDefault());
        }
        catch (JsonException exception)
        {
            var error = JsonSerializer.Deserialize<IDictionary<string, string>>(content);

            return Unauthorized(new { message = error?["message"] ?? exception.Message });
        }
    }
}
