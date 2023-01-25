using MediscreenAPI.Model.Entities;
using MediscreenAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MediscreenAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class AssessController : Controller
    {
        private readonly IReadHistoryService _readHistoryService;

        public AssessController(IReadHistoryService historyService)
        {
            _readHistoryService = historyService ?? throw new ArgumentNullException(nameof(IReadHistoryService));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> ById(int id)
        {
            List<History> history = await _readHistoryService.GetByPatIdAsync(id);

            if (history == null) return NotFound();

            return Ok("");
        }


        [HttpGet("{familyName}")]
        public async Task<ActionResult<string>> ByFamilyName(string familyName)
        {
            List<History> history = await _readHistoryService.GetByPatIdAsync(0);

            if (history == null) return NotFound();

            return Ok("");
        }
    }
}
