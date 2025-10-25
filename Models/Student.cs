namespace Identity.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string UserId { get; set; }   // foreign key from IdentityUser
        public ApplicationUser User { get; set; }

        public string University { get; set; }
        public string Faculty { get; set; }
        public string Department { get; set; }
        public string Year { get; set; }
        public string RegisterCourses { get; set; }
    }
}
