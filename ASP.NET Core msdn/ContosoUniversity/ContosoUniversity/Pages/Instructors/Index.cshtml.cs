using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.Models.SchoolViewModels;

namespace ContosoUniversity.Pages.Instructors
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public InstructorIndexData Instructor { get; set; }
        public int InstructorID { get; set; }
        public int CourseID { get; set; }

        public async Task OnGetAsync(int? id, int? courseID)
        {
            Instructor = new InstructorIndexData();
            Instructor.Instructors = await _context.Instructors
                .Include(i => i.OfficeAssignment)
                .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
                //    .Include(i => i.CourseAssignments)
                //        .ThenInclude(i => i.Course)
                //            .ThenInclude(i => i.Enrollments)
                //                .ThenInclude(i => i.Student)
                //.AsNoTracking()
                //Notice the preceding code comments out .AsNoTracking(). Navigation properties can only be explicitly loaded for tracked entities.
                .OrderBy(i => i.LastName)
                .ToListAsync();

            if (id != null)
            {
                InstructorID = id.Value;
                //Instructor instructor = Instructor.Instructors
                //    .Where(x => x.ID == id.Value).Single();
                Instructor instructor = Instructor.Instructors
                    .Single(x => x.ID == id.Value);
                Instructor.Courses = instructor.CourseAssignments
                    .Select(s => s.Course);
            }

            if (courseID != null)
            {
                CourseID = courseID.Value;
                //Instructor.Enrollments = Instructor.Courses
                //    .Where(x => x.CourseID == courseID)
                //    .Single().Enrollments;
                //Instructor.Enrollments = Instructor.Courses
                //    .Single(x => x.CourseID == courseID).Enrollments;

                //获取集合使用 Collection，单个对象使用 Reference
                var selectedCourse = Instructor.Courses.Single(x => x.CourseID == CourseID);
                await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();

                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
                }
                Instructor.Enrollments = selectedCourse.Enrollments;
            }
        }
    }
}
