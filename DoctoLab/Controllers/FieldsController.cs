using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctoLab.Controllers
{
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

        [HttpPost]
        
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

    }
}
