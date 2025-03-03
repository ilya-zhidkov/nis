using Nis.Api.Options;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Nis.Api.Authentication.Moodle.Policies.Handlers;

namespace Nis.Api.Controllers;

[Authorize(Policy = nameof(RequireMoodleAccount))]
public sealed class StudentsController(IOptions<MoodleOptions> options, HttpClient http) : BaseApiController
{
    private readonly MoodleOptions _options = options.Value;

    /// <summary>
    /// Retrieves a collection of publicly available Moodle students.
    /// </summary>
    /// <returns>Collection of Moodle students.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Index()
    {
        var (url, courseId, _, _, format) = _options;
        var response = await http.GetAsync($"{url}/webservice/rest/server.php?wstoken={Request.Headers.Authorization}&moodlewsrestformat={format}&wsfunction=core_enrol_get_enrolled_users&courseid={courseId}");
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            return Ok(JsonSerializer.Deserialize<IEnumerable<IDictionary<string, object>>>(content));
        }
        catch (JsonException exception)
        {
            return BadRequest(new { message = JsonSerializer.Deserialize<IDictionary<string, string>>(content)?["message"] ?? exception.Message });
        }
    }

    /// <summary>
    /// Gets a single publicly available Moodle student by its username.
    /// </summary>
    /// <param name="username" example="admin"></param>
    /// <returns>Single publicly available Moodle student.</returns>
    [HttpGet("{username}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Show(string username)
    {
        var (url, _, _, _, format) = _options;
        var response = await http.GetAsync($"{url}/webservice/rest/server.php?wstoken={Request.Headers.Authorization}&moodlewsrestformat={format}&wsfunction=core_user_get_users_by_field&field={nameof(username)}&values[0]={username}");
        var content = await response.Content.ReadAsStringAsync();

        try
        {
            return Ok(JsonSerializer.Deserialize<IEnumerable<IDictionary<string, object>>>(content));
        }
        catch (JsonException exception)
        {
            return BadRequest(new { message = JsonSerializer.Deserialize<IDictionary<string, string>>(content)?["message"] ?? exception.Message });
        }
    }
}
