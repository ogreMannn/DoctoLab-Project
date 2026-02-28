using DoctoLab.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }


    }
}
