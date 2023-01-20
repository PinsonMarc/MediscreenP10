using MediscreenAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediscreenAPI.Model
{
    public class PatientContext : DbContext
    {
        public PatientContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patient { get; set; }
    }
}