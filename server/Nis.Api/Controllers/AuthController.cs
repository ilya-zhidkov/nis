using Nis.Api.Options;
using System.Text.Json;
using Nis.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Nis.Api.Controllers;

public sealed class AuthController : BaseApiController
{
    private readonly HttpClient _http;
    private readonly MoodleOptions _options;

    public AuthController(IOptions<MoodleOptions> options, HttpClient http)
    {
        _http = http;
        _options = options.Value;
    }

    /// <summary>
    /// Authenticates user based on the existing credentials.
    /// </summary>
    /// <param name="body">JSON encoded request body.</param>
    /// <returns>Authentication token.</returns>
    [HttpPost, Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest body)
    {
        var (username, password) = body;
        var (url, _, service, _, _) = _options;

        var response = await _http.GetAsync($"{url}/login/token.php?username={username}&password={password}&service={service}");
        var result = JsonSerializer.Deserialize<IDictionary<string, string>>(await response.Content.ReadAsStringAsync())!;

        return result.ContainsKey("error")
            ? Unauthorized(new { message = result["error"] })
            : Ok(result);
    }
}
