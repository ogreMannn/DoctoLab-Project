using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.GTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
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

       


    }
}
