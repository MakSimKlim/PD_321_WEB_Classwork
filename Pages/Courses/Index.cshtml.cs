using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Courses
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public IndexModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        public IList<CourseViewModel> Courses { get; set; }
        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                    .Select
                    (
                        p => new CourseViewModel
                        {
                            CourseID = p.CourseId,
                            Title = p.Title,
                            Credits = p.Credits,
                            DepartmentName = p.Department.Name
                        }
                    )
                    .ToListAsync();
        }

        /*public IList<Course> Courses { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Courses = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .ToListAsync();


        }*/
    }
}
