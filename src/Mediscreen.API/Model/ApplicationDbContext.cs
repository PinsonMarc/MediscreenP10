using Microsoft.EntityFrameworkCore;

namespace MediscreenAPI.Model.Entities
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Patient> Trades { get; set; }
    }
}