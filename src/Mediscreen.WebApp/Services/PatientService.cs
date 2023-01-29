using Domain.DTOs;
using MediscreenAPI.Model.Entities;
using Newtonsoft.Json;

namespace MediscreenWepApp.Services
{
    public class PatientService : IPatientService
    {
        private readonly HttpClient _httpClient;

        public PatientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreatePatient(PatientDto dto)
            => await _httpClient.PostAsJsonAsync<PatientDto>(API.Patient.create, dto);

        public async Task<List<PatientDto>?> ReadAllPatients()
        {
            string responseString = await _httpClient.GetStringAsync(API.Patient.readAll);
            List<PatientDto>? res = JsonConvert.DeserializeObject<List<PatientDto>>(responseString); ;
            return res;
        }

        public async Task<PatientDto?> ReadPatient(int id)
        {
            string responseString = await _httpClient.GetStringAsync(API.Patient.Read(id));
            PatientDto? res = JsonConvert.DeserializeObject<PatientDto>(responseString); ;
            return res;
        }

        public async Task<HttpResponseMessage> UpdatePatient(PatientDto dto)
            => await _httpClient.PostAsJsonAsync<PatientDto>(API.Patient.update, dto);

        public async Task<HttpResponseMessage> DeletePatient(int id)
            => await _httpClient.DeleteAsync(API.Patient.Delete(id));

        public async Task<List<string>?> ReadHistory(int patId)
        {
            string responseString = await _httpClient.GetStringAsync(API.PatHistory.ReadByPatId(patId));
            List<string>? res = JsonConvert.DeserializeObject<List<string>>(responseString);
            return res;
        }

        public async Task<HttpResponseMessage> AddNote(NoteDto note)
            => await _httpClient.PostAsJsonAsync<NoteDto>(API.PatHistory.create, note);

        public async Task<string> AssessById(int patId)
            => await _httpClient.GetStringAsync(API.Assess.ById(patId));

        public async Task<string> AssessByName(string familyName)
            => await _httpClient.GetStringAsync(API.Assess.ByFamilyName(familyName));
    }
}