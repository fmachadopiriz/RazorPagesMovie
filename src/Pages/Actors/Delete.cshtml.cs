using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages_Actors
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesMovie.Models.ApplicationContext _context;

        public DeleteModel(RazorPagesMovie.Models.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Actor Actor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actor = await _context.GetActorByIdAsync(id);
            if (Actor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Actor = await _context.GetActorByIdAsync(id);

            if (Actor != null)
            {
                await _context.RemoveActorAsync(Actor);
            }

            return RedirectToPage("./Index");
        }
    }
}
