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
        private readonly ContosoUniversityContext _context;
        private readonly IConfiguration configuration;

        public IndexModel(ContosoUniversityContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        public string TitleSort { get; set; }      
        public string CreditsSort { get; set; }    
        public string CurrentFilter { get; set; }  
        public string CurrentSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;
        public PaginatedList<Course> PLCourses { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex, int? pageSize)
        {
            CurrentSort = sortOrder;
            TitleSort = sortOrder == "title_desc" ? "title_asc" : "title_desc";
            CreditsSort = sortOrder == "credits_desc" ? "credits_asc" : "credits_desc";
            if (searchString != null)
                pageIndex = 1;
            else
                searchString = CurrentFilter;

            CurrentFilter = searchString;

            IQueryable<Course> courses = _context.Courses;

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    courses = courses.OrderByDescending(c => c.Title);
                    break;
                case "title_asc":
                    courses = courses.OrderBy(c => c.Title);
                    break;
                case "credits_desc":
                    courses = courses.OrderByDescending(c => c.Credits);
                    break;
                case "credits_asc":
                    courses = courses.OrderBy(c => c.Credits);
                    break;
                default:    
                    courses = courses.OrderBy(c => c.Title);
                    break;
            }

            int currentPageSize = pageSize ?? 10;
            PageSize = currentPageSize;
            PLCourses = await PaginatedList<Course>.CreateAsync(
                courses.AsNoTracking(),
                pageIndex ?? 1,
                currentPageSize
            );
        }
    }
}
//public async Task OnGetAsync()
//{
//    Course = await _context.Courses.ToListAsync();
//}
