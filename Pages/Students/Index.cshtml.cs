using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.Extensions.Configuration;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;
        private readonly IConfiguration _configuration;

        public IndexModel(ContosoUniversity.Data.SchoolContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        // Движок Для создания сортировки
        public string LastNameSort { get; set; }
        public string FirstNameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        //public IList<Student> Students { get;set; } = default!; // замена на public PaginatedLis
        public PaginatedList<Student> Students { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;

            LastNameSort = String.IsNullOrEmpty(sortOrder) ? "last_name_desc":"";//Сортировка по убыванию (Descinding by FirstName)
            FirstNameSort  = sortOrder == "first_name_asc" ? "first_name_desc" : "first_name_asc";//Сортировка по убыванию (Descinding by LastName)
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null) pageIndex = 1;
            else searchString = currentFilter;

            CurrentFilter = searchString;

            IQueryable<Student> students = from s in _context.Students select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where
                        (
                             s => s.LastName.Contains(searchString)
                            || s.FirstName.Contains(searchString)
                        );
            }

            switch (sortOrder)
            {
                default: students = students.OrderBy(s => s.LastName); break;
                case "last_name_desc": students = students.OrderByDescending(s => s.LastName); break;
                
                case "first_name_asc": students = students.OrderBy(s => s.FirstName); break;
                case "first_name_desc": students = students.OrderByDescending(s => s.FirstName); break;
                
                case "Date": students = students.OrderBy(s => s.EnrollmentDate); break;
                case "date_desc": students = students.OrderByDescending(s => s.EnrollmentDate); break;
                
            }
            /// //////////////////////////////////////////

            int pageSize = _configuration.GetValue("PageSize", 4);
            Students = await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageIndex ?? 1, pageSize);
            //Students = await students.AsNoTracking().ToListAsync();
        }
    }
}
