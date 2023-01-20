using AutoMapper;
using Domain.DTOs;
using MediscreenAPI.Model.Entities;

namespace MediscreenAPI.Model
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Patient, PatientDto>().ReverseMap();
        }
    }
}