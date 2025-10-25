namespace Identity.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public bool SystemLogsAccess { get; set; }
        public string ActionsHistory { get; set; }
    }
}
