using Nis.Core.Persistence;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Patient = Nis.Api.Schemas.Patient;

namespace Nis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly DataContext _context;

        public PatientsController(DataContext context) => _context = context;

        /// <summary>
        /// Returns a list of all patients
        /// </summary>
        /// <response code="200">Patient list has been successfully returned.</response>
        [ProducesResponseType(typeof(Patient), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> Index() => Ok(await _context.Patients.ToListAsync());
    }
}
