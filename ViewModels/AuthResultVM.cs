namespace Identity.ViewModels
{
    public class AuthResultVM
    {
        public bool Success { get; set; }
        public string? Token { get; set; }
        public DateTime? Expiration { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public string? Role { get; set; }
        public string? UserId { get; set; }
        public string? FullName { get; set; }
    }
}
