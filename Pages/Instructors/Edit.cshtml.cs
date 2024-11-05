using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.Instructors
{
    public class EditModel : InstructorCoursesPageModel
    {
        private readonly ContosoUniversity.Data.SchoolContext _context;

        public EditModel(ContosoUniversity.Data.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Instructor Instructor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Instructor instructor = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (instructor == null) return NotFound();
            Instructor = instructor;
            PopulateAssignedCourseData(_context, Instructor);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.

        /*public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            return Page();
            

            _context.Attach(Instructor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InstructorExists(Instructor.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }*/

        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCourses)
        {
            if (id == null) return NotFound();

            Instructor instructorToUpdate = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (instructorToUpdate == null) return NotFound();

            if
            (
                await TryUpdateModelAsync<Instructor>
                (
                    instructorToUpdate,
                    "Instructor",
                    i => i.FirstMidName,
                    i => i.LastName,
                    i => i.HireDate,
                    i => i.OfficeAssignment
                )
            )
            {
                if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment?.Location))
                    instructorToUpdate.OfficeAssignment = null;

                UpdateInstructorCourses(selectedCourses, instructorToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");

            }
            UpdateInstructorCourses (selectedCourses, instructorToUpdate);
            PopulateAssignedCourseData(_context, instructorToUpdate);
            return Page();
        }

        public void UpdateInstructorCourses(string[] selectedCourses, Instructor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            HashSet<string> selectedCoursesHS = new HashSet<string>(selectedCourses);
            HashSet<int> instructorCoursesHS = new HashSet<int>(instructorToUpdate.Courses.Select(c => c.CourseId));

            foreach (Course course in _context.Courses)
            {
                if (selectedCoursesHS.Contains(course.CourseId.ToString())) 
                {
                    if(!instructorCoursesHS.Contains(course.CourseId))
                    instructorToUpdate.Courses.Add(course);
                }
                else 
                {
                    if (instructorCoursesHS.Contains(course.CourseId))
                        instructorToUpdate.Courses.Remove(
                            instructorToUpdate.Courses.Single(c => c.CourseId == course.CourseId)
                            );
                }
            }

        }

        private bool InstructorExists(int id)
        {
            return _context.Instructors.Any(e => e.ID == id);
        }
    }
}
