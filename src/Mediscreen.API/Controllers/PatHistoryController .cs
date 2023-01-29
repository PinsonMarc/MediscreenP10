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
        public async Task<ActionResult<History>> Read(string id)
        {
            History? history = await _historyService.GetAsync(id);

            if (history == null) return NotFound();

            return Ok(history);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<List<History>>> ReadByPatId(int patId)
        {
            List<History> history = await _historyService.GetByPatIdAsync(patId);

            if (history == null) return NotFound();

            return Ok(history);
        }

        [HttpPost]
        public async Task<IActionResult> Add(History newNote)
        {
            await _historyService.CreateAsync(newNote);

            return CreatedAtAction(nameof(Add), new { id = newNote.Id }, newNote);
        }

        [HttpPost("{id:length(24)}")]
        public async Task<IActionResult> Edit(string id, History updatedNote)
        {
            History? history = await _historyService.GetAsync(id);

            if (history == null) return NotFound();

            updatedNote.Id = history.Id;

            await _historyService.UpdateAsync(id, updatedNote);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            History? history = await _historyService.GetAsync(id);

            if (history == null) return NotFound();

            await _historyService.RemoveAsync(id);

            return NoContent();
        }
    }
}
