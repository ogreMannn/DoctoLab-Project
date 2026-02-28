using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.GTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorGetDto>>> GetAll()
        {
            var doctors = await _context.Doctors
                .Select(x => new DoctorGetDto
                {

                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Age = x.Age,
                    Description = x.Description,
                    FilePath = x.FilePath,
                    FieldId = x.FieldId,
                    FieldName = x.field.Name,
                    HospitalId = x.HospitalId,
                    HospitalName = x.hospital.Name


                }).ToListAsync();

            return Ok(doctors);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<DoctorGetDto>> GetById(int id)
        {
            var doctor = await _context.Doctors
                .Where(x => x.Id == id)
                .Select(x => new DoctorGetDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surname = x.Surname,
                    Age = x.Age,
                    Description = x.Description,
                    FilePath = x.FilePath,
                    FieldId = x.FieldId,
                    FieldName = x.field.Name,
                    HospitalId = x.HospitalId,
                    HospitalName = x.hospital.Name


                })
                .FirstOrDefaultAsync();
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }

        [HttpPost]
        public async Task<ActionResult<DoctorCreateDto>> Create(DoctorCreateDto dto)
        {
            var doctor = new Doctor
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Age = dto.Age,
                Description = dto.Description,
                FilePath = dto.FilePath,
                FieldId = dto.FieldId,
                HospitalId = dto.HospitalId,
            };


            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            var result = new DoctorGetDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Age = doctor.Age,
                Description = doctor.Description,
                FilePath = doctor.FilePath,
                FieldId = doctor.FieldId,
                HospitalId = doctor.HospitalId,

            };

            return CreatedAtAction(nameof(GetById),
                new { id = doctor.Id }, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DoctorGetDto>> Delete(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return Ok("U deleted doctor successfully");

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<DoctorGetDto>> Update(int id , DoctorCreateDto updateDoctor)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NoContent();
            }

            doctor.Name = updateDoctor.Name;
            doctor.Surname = updateDoctor.Surname;
            doctor.Age = updateDoctor.Age;
            doctor.Description = updateDoctor.Description;
            doctor.FilePath = updateDoctor.FilePath;
            doctor.FieldId = updateDoctor.FieldId;
            doctor.HospitalId = updateDoctor.HospitalId;

            await _context.SaveChangesAsync();

            var result = new DoctorGetDto
            {
                Id = doctor.Id,
                Name = doctor.Name,
                Surname = doctor.Surname,
                Age = doctor.Age,
                Description = doctor.Description,
                FilePath = doctor.FilePath,
                FieldId = doctor.FieldId,
                HospitalId = doctor.HospitalId
            };

            return Ok(result);

        }


        

    }
}
