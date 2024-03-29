﻿using Domain.DTOs;
using MediscreenWepApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediscreenAPI.Controllers
{
    public class PatientController : Controller
    {
        private IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patient
        public async Task<IActionResult> Index()
        {
            return View(await _patientService.ReadAllPatients());
        }

        // GET: Patient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return await TryFindReturnView(id);
        }

        // GET: Patient/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Patient/Add
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Family,Given,Address,Phone,Dob,Sex")] PatientDto patient)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage result = await _patientService.CreatePatient(patient);
                if (result.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                return StatusCode((int)result.StatusCode);
            }
            return View(patient);
        }

        // GET: Patient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return await TryFindReturnView(id);
        }

        // POST: Patient/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Family,Given,Address,Phone,Dob,Sex,Id")] PatientDto patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage result = await _patientService.UpdatePatient(patient);
                if (result.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                return StatusCode((int)result.StatusCode);
            }
            return View(patient);
        }

        // GET: Patient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return await TryFindReturnView(id);
        }

        // POST: Patient/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage res = await _patientService.DeletePatient(id);
            if (res.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)res.StatusCode);
        }

        private async Task<IActionResult> TryFindReturnView(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            PatientDto? patient = await _patientService.ReadPatient((int)id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // GET: Patient/Assess/1
        public async Task<string> Assess(int id)
            => await _patientService.AssessById(id);
    }
}
