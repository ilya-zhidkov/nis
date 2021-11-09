using Nis.Core.Persistence;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Nis.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly DataContext _context;

        public PatientsController(DataContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> Index() => Ok(await _context.Patients.ToListAsync());
    }
}
