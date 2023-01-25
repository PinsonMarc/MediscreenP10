using AutoMapper;
using Domain.DTOs;
using MediscreenAPI.Model;
using MediscreenAPI.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediscreenAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PatientsController : Controller
    {
        private readonly PatientContext _context;
        protected IMapper _mapper;

        public PatientsController(PatientContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        // GET: Patients
        [HttpGet]
        public async Task<PatientDto[]> Read()
        {
            Patient[] patient = await _context.Patient.ToArrayAsync();

            return _mapper.Map<Patient[], PatientDto[]>(patient);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            if (_context.Patient == null) return Problem("Entity set 'PatientContext.Patient' is null.");

            Patient? res = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);

            if (res == null) return NotFound();
            return Ok(_mapper.Map<Patient?, PatientDto>(res));
        }

        // POST: Patients/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody, Bind("Family,Given,Address,Phone,Dob,Sex,Id")] PatientDto dto)
        {
            try
            {
                Patient patient = _mapper.Map<PatientDto, Patient>(dto);
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        // POST: Patients/Edit
        [HttpPost]
        public async Task<IActionResult> Edit([FromBody, Bind("Family,Given,Address,Phone,Dob,Sex,Id")] PatientDto dto)
        {
            try
            {
                Patient patient = _mapper.Map<PatientDto, Patient>(dto);
                _context.Update(patient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PatientExists(dto.Id))
                {
                    return NotFound();
                }
                else
                {
                    Conflict(ex);
                }
            }
            return NoContent();
        }

        // POST: Patients/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Patient == null)
            {
                return Problem("Entity set 'PatientContext.Patient'  is null.");
            }
            Patient? patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
