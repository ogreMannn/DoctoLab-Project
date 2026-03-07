using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    
    public class FieldsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FieldsController(ApplicationDbContext context)
        {
            _context = context;
        }

   
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FieldGetDto>>> GetAll()
        {
            var fields = await _context.Fields
                .Select(x => new FieldGetDto
                {
                    Id = x.Id,
                    Name = x.Name


                })
                .ToListAsync();


            return Ok(fields);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<FieldGetDto>>> GetById(int id)
        {
            var field = await _context.Fields.Where(x => x.Id == id).Select(x => new FieldGetDto
            {

                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefaultAsync();

            if(field == null)
            {
                return NotFound();
            }

            return Ok(field);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<FieldGetDto>> Create(FieldCreateDto dto)
        {
            var field = new Field
            {
                Name = dto.Name
            };

            await _context.Fields.AddAsync(field);
            await _context.SaveChangesAsync();


            var result = new FieldGetDto
            {
                Id = field.Id,
                Name = field.Name

            };

            return CreatedAtAction(nameof(GetAll), result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<FieldGetDto>> Delete (int id)
        {
            var field = await _context.Fields.FindAsync(id);

            if(field == null)
            {
                return NotFound();
            }

            var hasDoctors = await _context.Doctors.AnyAsync(x => x.FieldId == id);
            if (hasDoctors)
            {
                return BadRequest("Cannon delete field with doctors");
            }

            _context.Fields.Remove(field);
            await _context.SaveChangesAsync();
            return Ok("Field deleted successfully");

        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<FieldGetDto>> Update(int id , FieldCreateDto dto)
        {
            var field = await _context.Fields.FindAsync(id);

            if (field == null)
                return NotFound();

            field.Name = dto.Name;

            await _context.SaveChangesAsync();

            var result = new FieldGetDto
            {
                Id = field.Id,
                Name =  field.Name

            };

            return Ok(result);

        }
    }
}
