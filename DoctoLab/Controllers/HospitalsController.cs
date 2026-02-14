using DoctoLab.Contexts;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hospitals= await _context.Hospitals.ToListAsync();
            return Ok(hospitals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            
            var hospital = await _context.Hospitals.FirstOrDefaultAsync(x => x.Id == id);
            
            if(hospital == null)
            {
                return NotFound();
            }

            return Ok(hospital);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Hospital hospital)
        {
            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById),new { id = hospital.Id },hospital);
        }


    }
}
