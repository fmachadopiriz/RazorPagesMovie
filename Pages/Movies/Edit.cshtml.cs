using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public IEnumerable<Actor> Actors { get; set; }

        public IEnumerable<Actor> AllActors { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the first movie with ID equal to received id. Include the appeareances of actors in that
            // movie. Then include the actors in the appearences of the movie.
            Movie = await _context.Movies
                .Where(m => m.ID == id)
                .Include(l => l.Location)
                .Include(c => c.Appeareances)
                    .ThenInclude(a => a.Actor)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Movie == null)
            {
                return NotFound();
            }

            // Populate the list of actors in the viewmodel with the actors of the movie.
            this.Actors = Movie.Appeareances
                .Select(a => a.Actor);

            string nameFilter = "";
            if (this.SearchString != null)
            {
                nameFilter = this.SearchString.ToUpper();
            }

            // Populate the list of all other actors with all actors not included in the movie's actors and
            // included in the search filter.
            this.AllActors = await _context.Actors
                .Where(a =>!Actors.Contains(a))
                .Where(a => !string.IsNullOrEmpty(nameFilter) ? a.Name.ToUpper().Contains(nameFilter) : true)
                .ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var movieToUpdate = await _context.Movies
                .Include(l => l.Location)
                .Include(a => a.Appeareances)
                    .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.ID == id);


            if (await TryUpdateModelAsync<Movie>(
                movieToUpdate,
                "Movie",
                i => i.Title, i => i.ReleaseDate,
                i => i.Price, i => i.Genre,
                i => i.Location))
            {
                if (String.IsNullOrWhiteSpace(movieToUpdate.Location?.Name))
                {
                    movieToUpdate.Location = null;
                }

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(Movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteActorAsync(int id, int actorToDeleteID)
        {
            Movie movieToUpdate = await _context.Movies
                .Include(l => l.Location)
                .Include(a => a.Appeareances)
                    .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.ID == id);

            await TryUpdateModelAsync<Movie>(movieToUpdate);

            var actorToDelete = movieToUpdate.Appeareances.Where(a => a.ActorID == actorToDeleteID).FirstOrDefault();
            if (actorToDelete != null)
            {
                movieToUpdate.Appeareances.Remove(actorToDelete);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect(Request.Path + $"?id={id}");
        }

        public async Task<IActionResult> OnPostAddActorAsync(int? id, int? actorToAddID)
        {
            Movie movieToUpdate = await _context.Movies
                .Include(a => a.Appeareances)
                    .ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(m => m.ID == Movie.ID);

            await TryUpdateModelAsync<Movie>(movieToUpdate);


            if (actorToAddID != null)
            {
                Actor actorToAdd = await _context.Actors.Where(a => a.ID == actorToAddID).FirstOrDefaultAsync();
                if (actorToAdd != null)
                {
                    var appereanceToAdd = new Appereance() {
                        ActorID = actorToAddID.Value,
                        Actor = actorToAdd,
                        MovieID = movieToUpdate.ID,
                        Movie = movieToUpdate };
                    movieToUpdate.Appeareances.Add(appereanceToAdd);
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect(Request.Path + $"?id={id}");
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }
    }
}
