using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Nis.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces(Application.Json)]
public abstract class BaseApiController : ControllerBase
{
    // ...
}
