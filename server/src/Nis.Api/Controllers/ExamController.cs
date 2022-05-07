using QuestPDF.Fluent;
using Nis.Api.Helpers.Pdf;
using Nis.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Nis.Api.Controllers;

public class ExamController : BaseApiController
{
    private readonly IHostEnvironment _env;

    public ExamController(IHostEnvironment env) => _env = env;

    [HttpPost, Route("pdf")]
    public IActionResult ExamResult([FromBody] ExamResult examResult)
    {
        var pdfPath = $"{_env.ContentRootPath}/wwwroot/pdf/{examResult.Student.FirstName}-{examResult.Student.LastName}.pdf";

        var document = new ExamResultDocument(examResult);
        document.GeneratePdf($"{pdfPath}");

        return System.IO.File.Exists($"{pdfPath}")
            ? Ok(new { message = "Pdf úspěšně vytvořeno" })
            : BadRequest(new { message = "Pdf nebylo vytvořené" });
    }
}
