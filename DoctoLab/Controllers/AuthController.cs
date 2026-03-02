using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AuthController(UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("Email already exists");

         

            var patient = new Patient
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Age = dto.Age
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            var user = new AppUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                Role = nameof(UserRole.Patient),
                PatientId = patient.Id,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Registered successfully");


        }

       
        
    }
}
