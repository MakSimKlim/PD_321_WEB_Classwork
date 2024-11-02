using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Budget { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")] // Дата существования кафедры
        public DateTime StartDate { get; set; }

        public int? InstructorID { get; set; }

        //Navigation properties
        public Instructor Administrator { get; set; } //Начальник кафедры (завкафедры)
        public ICollection<Course> Courses { get; set; }
    }
}
