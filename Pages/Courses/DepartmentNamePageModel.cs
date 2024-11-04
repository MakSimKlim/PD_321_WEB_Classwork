using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Pages.Courses
{
    public class DepartmentNamePageModel:PageModel
    {
        public SelectList DepartmentNameSL { get; set; }
        public void PopulateDepartmentDropdownList(SchoolContext context, object selectedDepartment = null)
        {
            IEnumerable<Department> departmentsQuery = from d in context.Departments
                                           orderby d.Name
                                           select d;
            DepartmentNameSL = new SelectList
                (
                    departmentsQuery,
                    nameof(Department.DepartmentID),
                    nameof(Department.Name),
                    selectedDepartment
                );
        }
    }
}
