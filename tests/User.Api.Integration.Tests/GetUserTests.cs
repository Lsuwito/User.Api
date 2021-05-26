using System;
using System.Threading.Tasks;
using Xunit;

namespace User.Api.Integration.Tests
{
    public class GetUserTests : IntegrationTestsBase
    {
        [Fact(DisplayName = "When GET a user, should get OK status code and user json")]
        public async Task GetUser()
        {
            // create a user
            var email = $"{Guid.NewGuid()}@skitterbytes.com";
            var createdUser = await CreateUserInternal(email, "Admin", "Active");

            // get the user
            var user = await GetUserInternal(createdUser.UserId);
            
            Assert.NotNull(user);
            Assert.Equal(createdUser.UserId, user.UserId);
            Assert.Equal(createdUser.Email, user.Email);
            Assert.Equal(createdUser.Role, user.Role);
            Assert.Equal(createdUser.Status, user.Status);
        }
    }
}