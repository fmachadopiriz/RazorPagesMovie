using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMovie.Models.MoviesViewModel;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Models.RazorPagesMovieContext context)
        {
            _context = context;
        }

        //public IList<Movie> Movie { get; set; }
        public MovieIndexData Movie { get; set; }

        public int MovieID { get; set; }

        public int ActorID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public async Task OnGetAsync(int? id, int? actorID)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movies
                                            orderby m.Genre
                                            select m.Genre;
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());

            Movie = new MovieIndexData();
            Movie.Movies = await _context.Movies
                .Where(s => !string.IsNullOrEmpty(SearchString) ? s.Title.Contains(SearchString) : true)
                .Where(x => !string.IsNullOrEmpty(MovieGenre) ? x.Genre == MovieGenre : true)
                .Include(l => l.Location)
                .Include(c => c.Appeareances)
                    .ThenInclude(c => c.Actor)
                .AsNoTracking()
                .ToListAsync();

            if (id != null)
            {
                MovieID = id.Value;
                Movie movie = Movie.Movies.Where(m => m.ID == id.Value).Single();
                Movie.Actors = movie.Appeareances.Select(a => a.Actor);
            }

            if (actorID != null)
            {
                ActorID = id.Value;
            }
        }
    }
}