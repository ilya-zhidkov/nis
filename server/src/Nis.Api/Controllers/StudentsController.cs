using Nis.Api.Options;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Nis.Api.Controllers;

public class StudentsController : BaseApiController
{
    private readonly HttpClient _http;
    private readonly MoodleOptions _options;

    public StudentsController(IOptions<MoodleOptions> options, HttpClient http)
    {
        _http = http;
        _options = options.Value;
    }

    [HttpGet]
    public async Task<IActionResult> Index([FromHeader] string token)
    {
        var (url, courseId, _, _, format) = _options;

        var response = await _http.GetAsync(
            $"{url}/webservice/rest/server.php?wstoken={token}&moodlewsrestformat={format}&wsfunction=core_enrol_get_enrolled_users&courseid={courseId}"
        );

        var content = await response.Content.ReadAsStringAsync();

        try
        {
            return Ok(JsonSerializer.Deserialize<IEnumerable<IDictionary<string, object>>>(content));
        }
        catch (JsonException exception)
        {
            var error = JsonSerializer.Deserialize<IDictionary<string, string>>(content);

            return BadRequest(new { message = error?["message"] ?? exception.Message });
        }
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> Show([FromHeader] string token, string username)
    {
        var (url, _, _, _, format) = _options;

        var response = await _http.GetAsync(
            $"{url}/webservice/rest/server.php?wstoken={token}&moodlewsrestformat={format}&wsfunction=core_user_get_users_by_field&field={nameof(username)}&values[0]={username}"
        );

        var content = await response.Content.ReadAsStringAsync();

        try
        {
            return Ok(JsonSerializer.Deserialize<IEnumerable<IDictionary<string, object>>>(content));
        }
        catch (JsonException exception)
        {
            var error = JsonSerializer.Deserialize<IDictionary<string, string>>(content);

            return BadRequest(new { message = error?["message"] ?? exception.Message });
        }
    }
}
