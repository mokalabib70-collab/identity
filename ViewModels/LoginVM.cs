using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; } = string.Empty; // email OR studentId

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
