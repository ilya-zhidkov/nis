using Nis.Api.Models;
using QuestPDF.Fluent;
using Nis.Api.Options;
using System.Text.Json;
using Nis.Api.Helpers.Pdf;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using static System.Net.Mime.MediaTypeNames;
using Nis.Api.Authentication.Moodle.Policies.Handlers;

namespace Nis.Api.Controllers;

[Authorize(Policy = nameof(RequireMoodleAccount))]
public sealed class ExamsController(
    HttpClient http,
    IOptions<MoodleOptions> options,
    IWebHostEnvironment environment
) : BaseApiController
{
    private readonly MoodleOptions _options = options.Value;

    /// <summary>
    /// Gets a file attachment of the submission by the file name.
    /// </summary>
    /// <param name="fileName" example="Ilya-Zhidkov.pdf">File name that matches the student's name.</param>
    /// <returns>PDF file of the submission.</returns>
    [HttpGet("{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Show(string fileName)
    {
        var path = Path.Combine($"{environment.WebRootPath}", "pdf", fileName);

        return !System.IO.File.Exists(path)
            ? NotFound(new { message = $"Soubor '{fileName}' nebyl nalezen." })
            : File(await System.IO.File.ReadAllBytesAsync(path), Application.Pdf, Path.GetFileName(path));
    }

    /// <summary>
    /// Uploads an exam to Moodle and stores it as an assignment attachment.
    /// </summary>
    /// <param name="body">JSON encoded request body.</param>
    /// <returns>Downloadable link of an exam in PDF form.</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/exams
    ///     {
    ///       "diet": "Tekutá",
    ///       "passed": true,
    ///       "diagnosis": "Infarkt myokardu",
    ///       "anamnesis": "Na oddělení JIP byl přivezen RZS 56letý pacient...",
    ///       "department": "Kardiologie",
    ///       "student": {
    ///         "firstName": "Ilya",
    ///         "lastName": "Zhidkov"
    ///       },
    ///       "scales": [
    ///         {
    ///           "name": "Najedení, napití",
    ///           "scaleType": 1,
    ///           "activities": [
    ///             {
    ///               "score": 10,
    ///               "name": "Samostatně bez pomoci"
    ///             }
    ///           ]
    ///         },
    ///         {
    ///           "name": "Fyzický stav",
    ///           "scaleType": 2,
    ///           "activities": [
    ///             {
    ///               "score": 4,
    ///               "name": "dobrý"
    ///             }
    ///           ]
    ///         },
    ///         {
    ///           "name": "Kolik plnohodnotných jídel sní pacient za den?",
    ///           "scaleType": 3,
    ///           "activities": [
    ///             {
    ///               "score": 3,
    ///               "name": "tři"
    ///             }
    ///           ]
    ///         },
    ///         {
    ///           "name": "Věk",
    ///           "scaleType": 4,
    ///           "activities": [
    ///             {
    ///               "score": 1,
    ///               "name": "75 a výše"
    ///             }
    ///           ]
    ///         }
    ///       ]
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Store([FromBody] Exam body)
    {
        var (firstName, lastName) = body.Student;
        var fileName = $"{firstName}-{lastName}.pdf".ToLower();
        var path = Path.Combine($"{environment.WebRootPath}", "pdf", fileName);

        var document = new ExamDocument(body);
        document.GeneratePdf(path);

        var form = new MultipartFormDataContent();
        var bytes = new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(path));
        bytes.Headers.ContentType = MediaTypeHeaderValue.Parse(Application.Pdf);
        form.Add(bytes, name: $"{firstName} {lastName}", fileName);

        var (url, _, _, _, format) = _options;

        var response = await http.PostAsync($"{url}/webservice/upload.php?token={Request.Headers.Authorization}", form);
        var metadata = JsonSerializer.Deserialize<IEnumerable<IDictionary<string, object>>>(await response.Content.ReadAsStringAsync())?.Single();

        await http.PostAsync(
            $"{url}/webservice/rest/server.php?wstoken={Request.Headers.Authorization}&wsfunction=mod_assign_save_submission&moodlewsrestformat={format}&assignmentid=1&plugindata[onlinetext_editor][text]={body.Anamnesis}&plugindata[onlinetext_editor][format]=1&plugindata[onlinetext_editor][itemid]=0&plugindata[files_filemanager]={metadata?["itemid"]}",
            content: null
        );

        return System.IO.File.Exists(path)
            ? CreatedAtAction(
                actionName: nameof(Show),
                routeValues: new { fileName },
                value: new { path = $"{HttpContext.Request.GetDisplayUrl()}/{fileName}" }
            )
            : BadRequest(new { message = "PDF nebylo vytvořené. Ověřte správnost zkoušky." });
    }
}
