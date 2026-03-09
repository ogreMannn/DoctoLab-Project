using AutoMapper;
using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.GTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DoctorsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        
        public async Task<ActionResult<IEnumerable<DoctorGetDto>>> GetAll()
        {
            var doctors = await _context.Doctors
               .Include(x => x.field)
               .Include(x => x.hospital)
               .ToListAsync();

            var result = _mapper.Map<List<DoctorGetDto>>(doctors);
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<DoctorGetDto>> GetById(int id)
        {
            var doctor = await _context.Doctors
                .Include(x => x.field)
                .Include(x => x.hospital)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (doctor == null)
                return NotFound();

            var result = _mapper.Map<DoctorGetDto>(doctor);

            return Ok(result);
        }

        

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<DoctorCreateDto>> Create(DoctorCreateDto dto)
        {
            var doctor = _mapper.Map<Doctor>(dto);

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<DoctorGetDto>(doctor);

            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DoctorGetDto>> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return Ok("Doctor deleted successfully");

        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<DoctorGetDto>> Update(int id , DoctorCreateDto dto)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
                return NotFound();

            _mapper.Map(dto, doctor);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<DoctorGetDto>(doctor));

        }


        

    }
}
