using DoctoLab.Contexts;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController(ApplicationDbContext _context) : ControllerBase
    {
       


        
    }
}
