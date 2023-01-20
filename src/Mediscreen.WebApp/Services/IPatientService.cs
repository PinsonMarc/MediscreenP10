using Domain.DTOs;

namespace MediscreenWepApp.Services
{
    public interface IPatientService
    {
        Task<HttpResponseMessage> Create(PatientDto dto);
        Task<HttpResponseMessage> Delete(int id);
        Task<PatientDto> Read(int id);
        Task<List<PatientDto>> ReadAll();
        Task<HttpResponseMessage> Update(PatientDto dto);
    }
}