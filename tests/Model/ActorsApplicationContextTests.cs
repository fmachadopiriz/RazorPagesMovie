using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Tests
{
    public class ActorsApplicationContextTests
    {
        [Fact]
        public async Task ActorsAreRetrievedTest()
        {
            using (var db = new ApplicationContext(Utilities.TestDbContextOptions()))
            {
                // Arrange: seed database with actors
                var expectedActors = SeedData.GetSeedingActors();
                await db.AddRangeAsync(expectedActors);
                await db.SaveChangesAsync();

                // Act: retrieve seeded actors from database
                var result = await db.GetActorsAsync();

                // Assert: seeded and retrieved actors match
                var actualActors = Assert.IsAssignableFrom<List<Actor>>(result);
                Assert.Equal(
                    expectedActors.OrderBy(a => a.ID).Select(a => a.Name),
                    actualActors.OrderBy(a => a.ID).Select(a => a.Name));
            }
        }
    }
}