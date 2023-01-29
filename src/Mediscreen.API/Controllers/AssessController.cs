using MediscreenAPI.Model;
using MediscreenAPI.Model.Entities;
using MediscreenAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MediscreenAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class AssessController : Controller
    {
        private readonly PatientContext _context;
        private readonly IReadHistoryService _readHistoryService;
        private readonly RiskRules _riskRules;

        public AssessController(IReadHistoryService historyService, PatientContext context, IOptions<RiskRules> riskRules)
        {
            _readHistoryService = historyService ?? throw new ArgumentNullException(nameof(IReadHistoryService));
            _riskRules = riskRules.Value ?? throw new ArgumentNullException(nameof(IOptions<RiskRules>));
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> ById(int id)
        {
            Patient? res = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);
            if (res == null) return NotFound();
            await AssessPatient(res);
            //if (history == null) return NotFound();

            return Ok("");
        }


        [HttpGet("{familyName}")]
        public async Task<ActionResult<string>> ByFamilyName(string familyName)
        {
            familyName = familyName.Trim();
            Patient? res = await _context.Patient.FirstOrDefaultAsync(m => m.Family == familyName);
            if (res == null) return NotFound();

            List<History> history = await _readHistoryService.GetByPatIdAsync(0);

            if (history == null) return NotFound();

            return Ok("");
        }

        private async Task AssessPatient(Patient pat)
        {
            List<History> history = await _readHistoryService.GetByPatIdAsync(pat.Id);
            Sex sex = pat.Sex;


        }
    }
}
