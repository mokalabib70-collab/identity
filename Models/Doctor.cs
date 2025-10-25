namespace Identity.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string University { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string Specialization { get; set; }
    }
}
