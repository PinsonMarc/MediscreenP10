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
        public async Task<List<History>> Get()
        {
            return await _historyService.GetAsync();
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<History>> Get(string id)
        {
            History? history = await _historyService.GetAsync(id);

            if (history is null)
            {
                return NotFound();
            }

            return history;
        }

        [HttpPost]
        public async Task<IActionResult> Post(History newBook)
        {
            await _historyService.CreateAsync(newBook);

            return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
        }

        [HttpPost("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, History updatedHistory)
        {
            History? history = await _historyService.GetAsync(id);

            if (history is null)
            {
                return NotFound();
            }

            updatedHistory.Id = history.Id;

            await _historyService.UpdateAsync(id, updatedHistory);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            History? history = await _historyService.GetAsync(id);

            if (history is null)
            {
                return NotFound();
            }

            await _historyService.RemoveAsync(id);

            return NoContent();
        }
    }
}
