namespace Identity.Models
{
    public class Support
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Department { get; set; }
        public bool IsOnline { get; set; }
        public string LastTicketLocal { get; set; }
        public int TicketsHandled { get; set; }
    }
}
