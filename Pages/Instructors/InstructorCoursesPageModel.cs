using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Pages.Instructors
{
    public class InstructorCoursesPageModel:PageModel
    {
        public List<AssignedCourseData> AssignedCourseDataList { get; set; }
        public void PopulateAssignedCourseData(SchoolContext context, Instructor instructor)
        {
            DbSet<Course> allCourses = context.Courses;
            HashSet<int> instructorCourses = new HashSet<int>(instructor.Courses.Select(c => c.CourseId));
            AssignedCourseDataList = new List<AssignedCourseData>();
            foreach (Course course in allCourses)
            {
                AssignedCourseDataList.Add
                    (
                        new AssignedCourseData
                        {
                            CourseID = course.CourseId,
                            Title = course.Title,
                            Assigned = instructorCourses.Contains(course.CourseId)
                        }

                    );            
            }
        }
    }
}
