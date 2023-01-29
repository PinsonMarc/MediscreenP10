using MediscreenAPI.Model.Entities;
using MediscreenWepApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediscreenAPI.Controllers
{
    public class PatHistoryController : Controller
    {
        private IPatientService _patientService;

        public PatHistoryController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // GET: PatHistory/1
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            (int, List<string>?) history = ((int)id, await _patientService.ReadHistory((int)id));

            return View(history);
        }

        // POST: PatHistory/Add
        [HttpPost]
        public async Task<IActionResult> Add([Bind("PatId,Note")] NoteDto newNote)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage result = await _patientService.AddNote(newNote);
                if (result.IsSuccessStatusCode) return NoContent();
                return StatusCode((int)result.StatusCode);
            }
            return BadRequest();
        }
    }
}
