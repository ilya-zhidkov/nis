using Nis.Api.Options;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Nis.Api.Authentication.Moodle.Policies.Handlers;

namespace Nis.Api.Controllers;

[Authorize(Policy = nameof(RequireMoodleAccount))]
public sealed class CoursesController(HttpClient http, IOptions<MoodleOptions> options) : BaseApiController
{
    private readonly MoodleOptions _options = options.Value;

    /// <summary>
    /// Retrieves all publicly available Moodle courses.
    /// </summary>
    /// <returns>Collection of course objects.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Index()
    {
        var (url, _, _, _, format) = _options;

        var content = await (await http
            .GetAsync($"{url}/webservice/rest/server.php?wstoken={Request.Headers.Authorization}&moodlewsrestformat={format}&wsfunction=core_course_get_courses"))
            .Content.ReadAsStringAsync();

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
    /// Gets a single publicly available Moodle course by its identifier.
    /// </summary>
    /// <param name="id" example="6">Moodle course identifier.</param>
    /// <returns>Single course object.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Show(ushort id)
    {
        var (url, _, _, _, format) = _options;

        var content = await (await http
            .GetAsync($"{url}/webservice/rest/server.php?wstoken={Request.Headers.Authorization}&moodlewsrestformat={format}&wsfunction=mod_assign_get_assignments&courseids[0]={id}"))
            .Content.ReadAsStringAsync();

        try
        {
            return Ok(JsonSerializer.Deserialize<IDictionary<string, IEnumerable<IDictionary<string, object>>>>(content)?["courses"].FirstOrDefault());
        }
        catch (JsonException exception)
        {
            return BadRequest(new { message = JsonSerializer.Deserialize<IDictionary<string, string>>(content)?["message"] ?? exception.Message });
        }
    }
}
