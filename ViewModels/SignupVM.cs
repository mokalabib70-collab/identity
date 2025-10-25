using System.ComponentModel.DataAnnotations;

namespace Identity.ViewModels
{
    public class SignupVM
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string? PhoneNumber { get; set; }
        public string Role { get; set; } = "Student";

        public string? University { get; set; }
        public string? Faculty { get; set; }
        public string? Department { get; set; }
        public string? Year { get; set; } // للطالب
        public string? Specialization { get; set; }
        public string RegisterCourses { get; set; }

    }
}
