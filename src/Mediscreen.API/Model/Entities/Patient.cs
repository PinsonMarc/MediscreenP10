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
        public int Age
        {
            get
            {
                int age = DateTime.Today.Year - Dob.Year;
                if (Dob > DateTime.Today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
