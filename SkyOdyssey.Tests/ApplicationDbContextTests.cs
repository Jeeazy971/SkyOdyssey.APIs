using Microsoft.EntityFrameworkCore;
using SkyOdyssey.Data;
using SkyOdyssey.Models;
using System.Threading.Tasks;
using Xunit;

namespace SkyOdyssey.Tests
{
    public class ApplicationDbContextTests
    {
        private DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddUser_ShouldSaveUserToDatabase()
        {
            var options = CreateNewContextOptions();

            using (var context = new ApplicationDbContext(options))
            {
                var user = new User { Username = "testuser", Email = "test@example.com" };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var user = await context.Users.SingleOrDefaultAsync(u => u.Username == "testuser");
                Assert.NotNull(user);
                Assert.Equal("test@example.com", user.Email);
            }
        }
    }
}
