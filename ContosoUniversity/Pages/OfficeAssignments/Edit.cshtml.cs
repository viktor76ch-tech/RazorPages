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

namespace ContosoUniversity.Pages.OfficeAssignments
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Data.ContosoUniversityContext _context;

        public EditModel(ContosoUniversity.Data.ContosoUniversityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OfficeAssignment OfficeAssignment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officeassignment =  await _context.OfficeAssignments.FirstOrDefaultAsync(m => m.InstructorID == id);
            if (officeassignment == null)
            {
                return NotFound();
            }
            OfficeAssignment = officeassignment;
           ViewData["InstructorID"] = new SelectList(_context.Instructors, "Id", "FirstName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OfficeAssignment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeAssignmentExists(OfficeAssignment.InstructorID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OfficeAssignmentExists(int id)
        {
            return _context.OfficeAssignments.Any(e => e.InstructorID == id);
        }
    }
}
