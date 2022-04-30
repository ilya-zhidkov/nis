using QuestPDF.Fluent;
using Nis.Api.Helpers.Pdf;
using Nis.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Nis.Api.Controllers
{
    public class ExamController : BaseApiController
    {
        private readonly IHostEnvironment _env;

        public ExamController(IHostEnvironment env) => _env = env;

        [HttpPost, Route("/pdf")]
        public IActionResult QuizResult([FromBody] QuizResult quizResult)
        {
            var rootPath = _env.ContentRootPath;

            var document = new QuizResultDocument(quizResult);
            document.GeneratePdf($"{rootPath}/wwwroot/pdf/{quizResult.StudentFirstName}-{quizResult.StudentLastName}.pdf");

            return Ok(rootPath);
        }
    }
}
