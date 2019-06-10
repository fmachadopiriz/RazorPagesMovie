using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Models
{
    public class RazorPagesMovieContext : DbContext
    {
        public RazorPagesMovieContext (DbContextOptions<RazorPagesMovieContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesMovie.Models.Movie> Movies { get; set; }

        public DbSet<RazorPagesMovie.Models.Actor> Actors { get; set; }

        public DbSet<Appereance> Appereances { get; set; }

        public DbSet<Location> Location { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appereance>().ToTable("Appereance")
                 .HasKey(a => new { a.ActorID, a.MovieID });
        }
    }
}