using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Areas.Identity.Data;

namespace RazorPagesMovie.Models
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


            builder.Entity<Appereance>().ToTable("Appereance")
                 .HasKey(a => new { a.ActorID, a.MovieID });
        }

        public DbSet<RazorPagesMovie.Models.Movie> Movies { get; set; }

        public DbSet<RazorPagesMovie.Models.Actor> Actors { get; set; }

        public DbSet<Appereance> Appereances { get; set; }

        public DbSet<Location> Location { get; set; }
    }
}
