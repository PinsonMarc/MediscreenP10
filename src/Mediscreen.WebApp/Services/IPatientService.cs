﻿using Domain.DTOs;

namespace MediscreenWepApp.Services
{
    public interface IPatientService
    {
        Task<HttpResponseMessage> CreatePatient(PatientDto dto);
        Task<HttpResponseMessage> DeletePatient(int id);
        Task<PatientDto?> ReadPatient(int id);
        Task<List<PatientDto>?> ReadAllPatients();
        Task<HttpResponseMessage> UpdatePatient(PatientDto dto);
        Task<List<string>?> ReadHistory(int patId);
        Task<HttpResponseMessage> AddNote(int patId, string note);
    }
}