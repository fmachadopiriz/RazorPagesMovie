using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace RazorPagesMovie.Models
{
    public static class SeedData
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<RazorPagesMovieContext>>()))
            {
                SeedActors(context);
                SeedMovies(context);
                SeedAppereances(context);
            }
        }

        private static void SeedMovies(RazorPagesMovieContext context)
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "When Harry Met Sally",
                    ReleaseDate = DateTime.Parse("1989-2-12"),
                    Genre = "Romantic Comedy",
                    Price = 7.99M,
                    Rating = "R"
                },

                new Movie
                {
                    Title = "Ghostbusters",
                    ReleaseDate = DateTime.Parse("1984-3-13"),
                    Genre = "Comedy",
                    Price = 8.99M,
                    Rating = "R"
                },

                new Movie
                {
                    Title = "Ghostbusters 2",
                    ReleaseDate = DateTime.Parse("1986-2-23"),
                    Genre = "Comedy",
                    Price = 9.99M,
                    Rating = "R"
                },

                new Movie
                {
                    Title = "Rio Bravo",
                    ReleaseDate = DateTime.Parse("1959-4-15"),
                    Genre = "Western",
                    Price = 3.99M,
                    Rating = "R"
                }
            );
            context.SaveChanges();
        }

        private static void SeedActors(RazorPagesMovieContext context)
        {
            // Look for any actor.
            if (context.Actor.Any())
            {
                return;   // DB has been seeded
            }

            context.Actor.AddRange(
                new Actor
                {
                    Name = "Billy Crystal",
                    BirthDate = DateTime.Parse("1948-3-14"),
                    AwardedBestActor = true
                },

                new Actor
                {
                    Name = "Meg Ryan",
                    BirthDate = DateTime.Parse("1961-11-19"),
                    AwardedBestActor = true
                },

                new Actor
                {
                    Name = "Bill Murray",
                    BirthDate = DateTime.Parse("1950-9-21"),
                    AwardedBestActor = true
                },

                new Actor
                {
                    Name = "Dan Aykroyd",
                    BirthDate = DateTime.Parse("1952-7-1"),
                    AwardedBestActor = false
                },

                new Actor
                {
                    Name = "Sigourney Weaver",
                    BirthDate = DateTime.Parse("1949-10-8"),
                    AwardedBestActor = false
                },

                new Actor
                {
                    Name = "John Wayne",
                    BirthDate = DateTime.Parse("1907-5-26"),
                    AwardedBestActor = false
                },

                new Actor
                {
                    Name = "Dean Martin",
                    BirthDate = DateTime.Parse("1917-7-7"),
                    AwardedBestActor = false
                }
            );
            context.SaveChanges();
        }

        private static void SeedAppereances(RazorPagesMovieContext context)
        {
            // Look for any appereance.
            if (context.Appereances.Any())
            {
                return;   // DB has been seeded
            }

            var appereances = new Appereance[]
            {
                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Billy Crystal").ID,
                    MovieID = context.Movie.Single(m => m.Title == "When Harry Met Sally").ID
                },

                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Meg Ryan").ID,
                    MovieID = context.Movie.Single(m => m.Title == "When Harry Met Sally").ID
                },

                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Bill Murray").ID,
                    MovieID = context.Movie.Single(m => m.Title == "Ghostbusters").ID
                },

                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Dan Aykroyd").ID,
                    MovieID = context.Movie.Single(m => m.Title == "Ghostbusters").ID
                },

                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Sigourney Weaver").ID,
                    MovieID = context.Movie.Single(m => m.Title == "Ghostbusters").ID
                },

                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Bill Murray").ID,
                    MovieID = context.Movie.Single(m => m.Title == "Ghostbusters 2").ID
                },

                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Dan Aykroyd").ID,
                    MovieID = context.Movie.Single(m => m.Title == "Ghostbusters 2").ID
                },

                new Appereance
                {
                    ActorID = context.Actor.Single(a => a.Name == "Sigourney Weaver").ID,
                    MovieID = context.Movie.Single(m => m.Title == "Ghostbusters 2").ID
                },

                 new Appereance
                 {
                     ActorID = context.Actor.Single(a => a.Name == "John Wayne").ID,
                     MovieID = context.Movie.Single(m => m.Title == "Rio Bravo").ID
                 },

                 new Appereance {
                     ActorID = context.Actor.Single(a => a.Name == "Dean Martin").ID,
                     MovieID = context.Movie.Single(m => m.Title == "Rio Bravo").ID
                 }
            };

            foreach (Appereance a in appereances)
            {
                context.Appereances.Add(a);
            }
            context.SaveChanges();
        }
    }
}


