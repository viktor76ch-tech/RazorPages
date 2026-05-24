using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;

namespace ContosoUniversity.Pages.OfficeAssignments
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;

        public DetailsModel(ContosoUniversity.Data.ContosoUniversityContext context)
        {
            _context = context;
        }

        public OfficeAssignment OfficeAssignment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeassignment = await _context.OfficeAssignments.FirstOrDefaultAsync(m => m.InstructorID == id);
            if (officeassignment == null)
            {
                return NotFound();
            }
            else
            {
                OfficeAssignment = officeassignment;
            }
            return Page();
        }
    }
}
