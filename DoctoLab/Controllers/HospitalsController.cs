using DoctoLab.Contexts;
using DoctoLab.GTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HospitalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HospitalGetDto>>> GetAll()
        {
            var hospitals = await _context.Hospitals
                .Select(x => new HospitalGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                })
                .ToListAsync();

            return Ok(hospitals);
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<HospitalGetDto>> GetById(int id)
        {
            var hospital = await _context.Hospitals
                .Where(x => x.Id == id)
                .Select(x => new HospitalGetDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address
                })
                .FirstOrDefaultAsync();

            if (hospital == null)
                return NotFound();

            return Ok(hospital);
        }

        
        [HttpPost]
        public async Task<ActionResult<HospitalGetDto>> Create(HospitalCreateDto dto)
        {
            var hospital = new Hospital
            {
                Name = dto.Name,
                Address = dto.Address
            };

            await _context.Hospitals.AddAsync(hospital);
            await _context.SaveChangesAsync();

            var result = new HospitalGetDto
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address
            };

            return CreatedAtAction(nameof(GetById),
                new { id = hospital.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HospitalGetDto>> Delete(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if(hospital == null)
            {
                return NotFound();
            }

            var hasDoctors = await _context.Doctors.AnyAsync(x => x.HospitalId == id);
            if (hasDoctors)
            {
                return BadRequest("U can not delete hospitals with doctors");
            }

            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return Ok("Hospital deleted successfully");
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<HospitalGetDto>> Update(int id, HospitalCreateDto updateHospital)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            if(hospital == null)
            {
                return NotFound();
            }

            hospital.Name = updateHospital.Name;
            hospital.Address = updateHospital.Address;
            await _context.SaveChangesAsync();

            var result = new HospitalGetDto
            {
                Id = hospital.Id,
                Name = hospital.Name,
                Address = hospital.Address

            };

            return Ok(result);
            
        }
        

        
    }
}