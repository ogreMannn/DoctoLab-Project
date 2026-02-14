using DoctoLab.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController(ApplicationDbContext _context) : ControllerBase
    {

    }
}
