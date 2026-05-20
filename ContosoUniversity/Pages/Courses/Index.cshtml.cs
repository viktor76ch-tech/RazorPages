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

        // Свойства для сортировки
        public string TitleSort { get; set; }      // для переключения сортировки по названию
        public string CreditsSort { get; set; }    // для переключения сортировки по кредитам
        public string CurrentFilter { get; set; }  // текущий поисковый запрос
        public string CurrentSort { get; set; }    // текущий тип сортировки

        public PaginatedList<Course> PLCourses { get; set; }
        // public IQueryable<Course> Courses { get; set; } = default!; // можно удалить, если не используется в представлении

        public async Task OnGetAsync(string sortOrder, string searchString, int? pageIndex)
        {
            // 1. Запоминаем текущую сортировку
            CurrentSort = sortOrder;

            // 2. Определяем параметры для ссылок (чтобы переключать направление)
            //    Если сейчас сортировка по названию по убыванию, то следующая ссылка будет "title_asc"
            TitleSort = sortOrder == "title_desc" ? "title_asc" : "title_desc";
            CreditsSort = sortOrder == "credits_desc" ? "credits_asc" : "credits_desc";

            // 3. Логика поиска (сохраняем запрос между страницами)
            if (searchString != null)
                pageIndex = 1;            // новый поиск – сброс на первую страницу
            else
                searchString = CurrentFilter;

            CurrentFilter = searchString;

            // 4. Базовый запрос
            IQueryable<Course> courses = _context.Courses;

            // 5. Фильтрация по названию курса
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(c => c.Title.Contains(searchString));
            }

            // 6. Сортировка
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
                default:    // по умолчанию сортировка по названию (возрастание)
                    courses = courses.OrderBy(c => c.Title);
                    break;
            }

            // 7. Пагинация
            int pageSize = configuration.GetValue("PageSize", 5);
            PLCourses = await PaginatedList<Course>.CreateAsync(
                courses.AsNoTracking(),
                pageIndex ?? 1,
                pageSize
            );
        }
    }
}
//public async Task OnGetAsync()
//{
//    Course = await _context.Courses.ToListAsync();
//}
