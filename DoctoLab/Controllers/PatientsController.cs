using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientGetDto>>> GetAll()
        {
            var patients = await _context.Patients
                .Select(x => new PatientGetDto
                {

                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Age = x.Age,
                   


                }).ToListAsync();

            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientGetDto>> GetById(int id)
        {
            var patient = await _context.Patients
                .Where(x => x.Id == id)
                .Select(x => new PatientGetDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Age = x.Age,
                   


                })
                .FirstOrDefaultAsync();
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<PatientCreateDto>> Create(PatientCreateDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Age = dto.Age,
               
            };


            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            var result = new PatientGetDto
            {
                Id = patient.Id,
                Name = patient.Name,
                Surname = patient.Surname,
                Age = patient.Age,
               

            };

            return CreatedAtAction(nameof(GetById),
                new { id = patient.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PatientGetDto>> Delete(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return Ok("U deleted doctor successfully");

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<PatientGetDto>> Update(int id, PatientCreateDto updatePatient)
        {
            var doctor = await _context.Patients.FindAsync(id);
            if (doctor == null)
            {
                return NoContent();
            }

            doctor.Name = updatePatient.Name;
            doctor.Surname = updatePatient.Surname;
            doctor.Age = updatePatient.Age;
            

            await _context.SaveChangesAsync();

            var result = new PatientGetDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Age = doctor.Age,
              
          
            };

            return Ok(result);

        }
    }
}
