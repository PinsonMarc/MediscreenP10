namespace MediscreenAPI.Model.Entities
{
    public class Patient : Entity
    {
        public string? Family { get; set; }
        public string? Given { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateOnly Dob { get; set; }
        public Sex Sex { get; set; }
    }
}
