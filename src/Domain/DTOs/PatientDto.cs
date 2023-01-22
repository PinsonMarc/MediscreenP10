using MediscreenAPI.Model;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public string? Family { get; set; }
        public string? Given { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        public Sex Sex { get; set; }
    }
}