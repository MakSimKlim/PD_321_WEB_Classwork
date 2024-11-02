using System.ComponentModel.DataAnnotations;
namespace ContosoUniversity.Models
{
    public enum Grade { A, B, C, D, F }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }


        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }


        //Navigation properties:
        public Course Course { get; set; } //внешние ключи
        public Student Student { get; set; } //внешние ключи
    }
}
