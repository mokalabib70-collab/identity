namespace Identity.Models
{
    public class Proctor
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string AssignedExams { get; set; }
        public string ReviewedAlerts { get; set; }
    }
}
