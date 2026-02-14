using DoctoLab.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController(ApplicationDbContext _context) : ControllerBase
    {

    }
}
