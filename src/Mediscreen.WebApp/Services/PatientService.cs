using Domain.DTOs;
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

        public async Task<HttpResponseMessage> Create(PatientDto dto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<PatientDto>(API.Patient.create, dto);

            return response;
        }

        public async Task<List<PatientDto>?> ReadAll()
        {
            string responseString = await _httpClient.GetStringAsync(API.Patient.readAll);
            List<PatientDto>? res = JsonConvert.DeserializeObject<List<PatientDto>>(responseString); ;
            return res;
        }

        public async Task<PatientDto?> Read(int id)
        {
            string responseString = await _httpClient.GetStringAsync(API.Patient.Read(id));
            PatientDto? res = JsonConvert.DeserializeObject<PatientDto>(responseString); ;
            return res;
        }

        public async Task<HttpResponseMessage> Update(PatientDto dto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<PatientDto>(API.Patient.update, dto);
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(API.Patient.Delete(id));
            return response;
        }
    }
}