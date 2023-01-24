namespace MediscreenAPI.Model.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string? Family { get; set; }
        public string? Given { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public DateTime Dob { get; set; }
        public Sex Sex { get; set; }
    }
}
