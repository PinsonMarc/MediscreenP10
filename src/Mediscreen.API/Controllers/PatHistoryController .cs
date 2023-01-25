using MediscreenAPI.Model.Entities;
using MediscreenAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediscreenAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PatHistoryController : Controller
    {
        private readonly HistoryService _historyService;

        public PatHistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Note>> Read(string id)
        {
            Note? history = await _historyService.GetAsync(id);

            if (history == null) return NotFound();

            return Ok(history);
        }

        public async Task<ActionResult<List<Note>>> ReadByPatId(int patId)
        {
            List<Note> history = await _historyService.GetByPatIdAsync(patId);

            if (history == null) return NotFound();

            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Note newNote)
        {
            await _historyService.CreateAsync(newNote);

            return NoContent();
        }

        [HttpPost("{id:length(24)}")]
        public async Task<IActionResult> Edit(string id, Note updatedNote)
        {
            Note? history = await _historyService.GetAsync(id);

            if (history == null) return NotFound();

            updatedNote.Id = history.Id;

            await _historyService.UpdateAsync(id, updatedNote);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            Note? history = await _historyService.GetAsync(id);

            if (history == null) return NotFound();

            await _historyService.RemoveAsync(id);

            return NoContent();
        }
    }
}
