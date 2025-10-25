namespace Identity.ViewModels
{
    public class UserResponceVM
    {
        public string Id { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLogin { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
