using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
      
        private readonly IConfiguration _config;

        public AuthController(UserManager<AppUser> userManager, ApplicationDbContext context,IConfiguration config)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
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

            return Ok(new
            {
                message = "Registered successfully"

            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password");

            var isValid = await _userManager.CheckPasswordAsync(

                user,
                dto.Password
                
             );

            if (!isValid)
                return Unauthorized("Invalid Email or password");

            var token = GenerateToken(user);

            return Ok(new
            {
                token = token,
                role = user.Role

            });


        }

        private string GenerateToken(AppUser user)
        {
            var jwt = _config.GetSection("Jwt");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(

                Encoding.UTF8.GetBytes(jwt["Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(

                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(

                    double.Parse(jwt["ExpireDays"])),

                signingCredentials:creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}
