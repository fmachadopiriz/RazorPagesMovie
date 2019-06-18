using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Locations
{
    public class DetailsModel : PageModel
    {
        private readonly RazorPagesMovie.Models.ApplicationContext _context;

        public DetailsModel(RazorPagesMovie.Models.ApplicationContext context)
        {
            _context = context;
        }

        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Location
                .Include(l => l.Movie).FirstOrDefaultAsync(m => m.MovieID == id);

            if (Location == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
