namespace Identity.Models
{
    public class LoginLog
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string Identifier { get; set; } = "";
        public DateTime LoginTime { get; set; }
        public bool IsSuccessful { get; set; }

        public string? IPAddress { get; set; }
        public string? FailureReason { get; set; }
    }
}
