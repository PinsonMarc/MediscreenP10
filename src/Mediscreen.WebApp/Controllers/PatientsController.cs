using Domain.DTOs;
using MediscreenWepApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediscreenAPI.Controllers
{
    public class PatientsController : Controller
    {
        private IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return View(await _patientService.ReadAll());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            return await TryFindReturnView(id);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Family,Given,Address,Phone,Dob,Sex")] PatientDto patient)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage result = await _patientService.Create(patient);
                if (result.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                return StatusCode((int)result.StatusCode);
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            return await TryFindReturnView(id);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Family,Given,Address,Phone,Dob,Sex,Id")] PatientDto patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                HttpResponseMessage result = await _patientService.Update(patient);
                if (result.IsSuccessStatusCode) return RedirectToAction(nameof(Index));
                return StatusCode((int)result.StatusCode);
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return await TryFindReturnView(id);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage res = await _patientService.Delete(id);
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
            PatientDto? patient = await _patientService.Read((int)id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }
    }
}
