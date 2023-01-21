﻿using AutoMapper;
using Domain.DTOs;
using MediscreenAPI.Model;
using MediscreenAPI.Model.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediscreenAPI.Controllers
{
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
        public async Task<PatientDto[]> Index()
        {
            Patient[] patient = await _context.Patient.ToArrayAsync();

            return _mapper.Map<Patient[], PatientDto[]>(patient);
        }

        // GET: Patients/5
        public async Task<PatientDto?> Index(int id)
        {
            if (_context.Patient == null) return null;

            Patient? res = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);

            return _mapper.Map<Patient?, PatientDto>(res);
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Family,Given,Address,Phone,Dob,Sex,Id")] PatientDto dto)
        {
            try
            {
                Patient patient = _mapper.Map<PatientDto, Patient>(dto);
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        // POST: Patients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Family,Given,Address,Phone,Dob,Sex,Id")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(patient);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!PatientExists(patient.Id))
                {
                    return NotFound();
                }
                else
                {
                    Conflict(ex);
                }
            }
            return Ok();
        }

        // POST: Patients/Delete/5
        [HttpDelete]
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
            return Ok();
        }

        private bool PatientExists(int id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
