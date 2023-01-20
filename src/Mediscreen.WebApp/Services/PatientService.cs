using Domain.DTOs;

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
            return null;
        }

        public async Task<List<PatientDto>> ReadAll()
        {
            return new List<PatientDto>();
        }

        public async Task<PatientDto> Read(int id)
        {
            return new PatientDto();
        }

        public async Task<HttpResponseMessage> Update(PatientDto dto)
        {
            return null;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            return null;
        }
    }
}