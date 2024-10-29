namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        // Navigation property
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}
