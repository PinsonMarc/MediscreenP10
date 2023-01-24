using MediscreenAPI.Model.Entities;
using MediscreenAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediscreenAPI.Controllers
{

    public class HistoryController : Controller
    {
        private readonly HistoryService _historyService;

        public HistoryController(HistoryService historyService)
        {
            _historyService = historyService;
        }

        [HttpGet]
        public async Task<List<History>> Index()
        {
            return await _historyService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<History>> Index(string id)
        {
            History? history = await _historyService.GetAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            return history;
        }

        [HttpPost]
        public async Task<IActionResult> Create(History newBook)
        {
            await _historyService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Create), new { id = newBook.Id }, newBook);
        }

        [HttpPost("{id:length(24)}")]
        public async Task<IActionResult> Edit(string id, History updatedHistory)
        {
            History? history = await _historyService.GetAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            updatedHistory.Id = history.Id;

            await _historyService.UpdateAsync(id, updatedHistory);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Deletea(string id)
        {
            History? history = await _historyService.GetAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            await _historyService.RemoveAsync(id);

            return NoContent();
        }
    }
}
