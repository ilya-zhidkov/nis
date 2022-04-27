using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nis.Api.Models.Requests;

namespace Nis.Api.Controllers
{
    public class AuthController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }
        
        [HttpGet, Route("/login")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromQuery] LoginRequest loginRequest)
        {
            var moodleSection = _configuration.GetSection("Moodle");
            var moodleUrl = moodleSection["Url"];
            var service = moodleSection["Service"];
            
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{moodleUrl}/login/token.php?username={loginRequest.UserName}&password={loginRequest.Password}&service={service}");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode) 
                return BadRequest(httpRequestMessage);

            return Ok(await httpResponseMessage.Content.ReadAsStreamAsync());
        }
    }
}
