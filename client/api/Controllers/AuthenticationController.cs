using Nis.Api.Options;
using System.Text.Json;
using Nis.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Nis.Api.Controllers;

public sealed class AuthenticationController(IOptions<MoodleOptions> options, HttpClient http) : BaseApiController
{
    private readonly MoodleOptions _options = options.Value;

    /// <summary>
    /// Authenticates user based on the Moodle credentials.
    /// </summary>
    /// <param name="body">JSON encoded request body.</param>
    /// <returns>Authentication token.</returns>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest body)
    {
        var (username, password) = body;
        var (url, _, service, _, _) = _options;

        var response = JsonSerializer.Deserialize<IDictionary<string, string>>(await (
            await http.GetAsync($"{url}/login/token.php?username={username}&password={password}&service={service}"))
            .Content.ReadAsStringAsync()
        )!;

        return response.TryGetValue("error", out var message) ? Unauthorized(new { message }) : Ok(response);
    }
}
