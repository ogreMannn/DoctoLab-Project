using DoctoLab.Contexts;
using DoctoLab.DTOs;
using DoctoLab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DoctoLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController: ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentGetDto>>> GetAll()
        {
            var appointments = await _context.Appointments
                .Select(x => new AppointmentGetDto
                {
                    Id = x.Id,
                    AppointmentData = x.AppointmentDate,
                    DoctorId = x.DoctorId,
                    DoctorName = x.Doctor.Name,
                    PatientId = x.PatientId,
                    PatientName = x.Patient !=null ? x.Patient.Name : null

                }).ToListAsync();

            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentGetDto>> GetById(int id)
        {
            var appointment = await _context.Appointments
                .Where(x => x.Id == id)
                .Select(x => new AppointmentGetDto()
                {
                    Id = x.Id,
                    AppointmentData = x.AppointmentDate,
                    PatientId = x.PatientId,
                    PatientName = x.Patient !=null ? x.Patient.Name : null,
                    DoctorId = x.DoctorId,
                    DoctorName = x.Doctor !=null ? x.Doctor.Name : null

                }).FirstOrDefaultAsync();

            if(appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetDoctorAppointments(int doctorId)
        {
            var appointments = await _context.Appointments.Where(x => x.DoctorId == doctorId).Select(x => new
            {

                x.Id,
                x.AppointmentDate,
                x.Status

            }).ToListAsync();

            return Ok(appointments);
        }


        [HttpPost]
        public async Task<ActionResult<AppointmentCreateDto>> Create(AppointmentCreateDto dto)
        {
            var doctorExists = await _context.Doctors.AnyAsync(x => x.Id == dto.DoctorId);
            var patientExists = await _context.Patients.AnyAsync(x => x.Id == dto.PatientId);

            if (!doctorExists || !patientExists)
                return BadRequest("Patient or Doctor not found");


            var isBooked = await _context.Appointments.AnyAsync(x =>

                x.DoctorId == dto.DoctorId &&
                x.AppointmentDate == dto.AppointmentDate &&
                x.Status != AppointmentStatus.Canceled);

            if (isBooked)
                return Conflict("Time slot already booked");

            var appointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                Status = AppointmentStatus.Pending

            };

            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<AppointmentGetDto>> Delete(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }

            appointment.Status = AppointmentStatus.Canceled;
            await _context.SaveChangesAsync();

            return NoContent();


        }


    }
}
