using ContosoUniversity.Data;
using ContosoUniversity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Pages.Enrollments
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;
        readonly IConfiguration configuration;
        public IndexModel(ContosoUniversity.Data.ContosoUniversityContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        public string CourseSort { get; set; }
        public string StudentSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 15;
        public PaginatedList<Enrollment> PLEnrollments { get; set; }
        public IQueryable<Enrollment> Enrollments { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex, int? pageSize)
        {
            CurrentSort = sortOrder;
            StudentSort = sortOrder == "student_desc" ? "student_asc" : "student_desc";
            CourseSort = sortOrder == "course_desc" ? "course_asc" : "course_desc";
            if (searchString != null) pageIndex = 1;
            else searchString = CurrentFilter;
            CurrentFilter = searchString;

            IQueryable<Enrollment> enrollments = _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course);

            if (!string.IsNullOrEmpty(searchString))
            {
                enrollments = enrollments.Where(e =>
                    e.Student.LastName.Contains(searchString) ||
                    e.Course.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "student_desc":
                    enrollments = enrollments.OrderByDescending(e => e.Student.LastName);
                    break;
                case "student_asc":
                    enrollments = enrollments.OrderBy(e => e.Student.LastName);
                    break;
                case "course_desc":
                    enrollments = enrollments.OrderByDescending(e => e.Course.Title);
                    break;
                case "course_asc":
                    enrollments = enrollments.OrderBy(e => e.Course.Title);
                    break;
                default:    // по умолчанию – сортировка по студенту (возрастание)
                    enrollments = enrollments.OrderBy(e => e.Student.LastName);
                    break;
            }

            int currentPageSize = pageSize ?? 10;
            PageSize = currentPageSize;
            PLEnrollments = await PaginatedList<Enrollment>.CreateAsync(
                enrollments.AsNoTracking(),
                pageIndex ?? 1,
                currentPageSize
            );
        }
        //public async Task OnGetAsync()
        //{
        //    Enrollments = await _context.Enrollments
        //        .Include(e => e.Course)
        //        .Include(e => e.Student)
        //        .ToListAsync();
        //}
    }
}
