using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Nis.Api.Models.Requests;

namespace Nis.Api.Controllers
{
    public class StudentsController : BaseApiController
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public StudentsController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost, Route("info")]
        [Produces("application/json")]
        public async Task<IActionResult> GetStudents([FromBody] StudentsRequest studentsRequest)
        {
            var moodleSection = _configuration.GetSection("Moodle");
            var moodleUrl = moodleSection["Url"];
            var moodleRestFormat = moodleSection["MoodleWsRestFormat"];
            
            var http = new HttpRequestMessage(
                HttpMethod.Get,
                $"{moodleUrl}/webservice/rest/server.php?wstoken={studentsRequest.Token}&moodlewsrestformat={moodleRestFormat}&wsfunction=core_enrol_get_enrolled_users&courseid={studentsRequest.CourseId}");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(http);

            if (!httpResponseMessage.IsSuccessStatusCode) 
                return BadRequest(http);

            return Ok(await  httpResponseMessage.Content.ReadAsStreamAsync());
        } 
    }

}
