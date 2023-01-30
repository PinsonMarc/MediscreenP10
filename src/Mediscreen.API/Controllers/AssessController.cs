using MediscreenAPI.Model;
using MediscreenAPI.Model.Entities;
using MediscreenAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

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
            try
            {
                Patient? res = await _context.Patient.FirstOrDefaultAsync(m => m.Id == id);
                if (res == null) return NotFound();

                string assessmentRes = await AssessPatient(res);
                return Ok(assessmentRes);
            }
            catch
            {
                return StatusCode(500);
            }
        }


        [HttpGet("{familyName}")]
        public async Task<ActionResult<string>> ByFamilyName(string familyName)
        {
            try
            {
                familyName = familyName.Trim();
                Patient? res = await _context.Patient.FirstOrDefaultAsync(m => m.Family == familyName);
                if (res == null) return NotFound();

                string assessmentRes = await AssessPatient(res);
                return Ok(assessmentRes);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        private async Task<string> AssessPatient(Patient patient)
        {
            string[] notes = await _readHistoryService.GetByPatIdAsync(patient.Id);
            int triggers = CountTriggerWords(string.Join(" ", notes));
            string riskName = _riskRules.EvaluateRisk(patient.Age, patient.Sex, triggers);

            return GetResultString(patient, riskName);
        }

        private string GetResultString(Patient patient, string riskName)
            => $"Patient : {patient.Given} {patient.Family} (age : {patient.Age}) diabetes assessment is: {riskName}";

        private int CountTriggerWords(string text)
        {
            Dictionary<string, int> keywordCount = new();

            foreach (string trigger in _riskRules.Triggers)
            {
                MatchCollection matches = Regex.Matches(text, trigger, RegexOptions.IgnoreCase);
                keywordCount.Add(trigger, matches.Count);
            }
            return keywordCount.Values.Sum();
        }
    }
}
