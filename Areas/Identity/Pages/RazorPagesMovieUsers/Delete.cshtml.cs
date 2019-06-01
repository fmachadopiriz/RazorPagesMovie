using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Areas.Identity.Data;

namespace RazorPagesMovie.Areas.Identity.Pages.RazorPagesMovieUsers
{
    public class DeleteModel : PageModel
    {
        private readonly RazorPagesMovie.Areas.Identity.Data.IdentityContext _context;

        public DeleteModel(RazorPagesMovie.Areas.Identity.Data.IdentityContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ApplicationUser RazorPagesMovieUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RazorPagesMovieUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (RazorPagesMovieUser == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RazorPagesMovieUser = await _context.Users.FindAsync(id);

            if (RazorPagesMovieUser != null)
            {
                _context.Users.Remove(RazorPagesMovieUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
