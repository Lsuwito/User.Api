using System;
using System.Threading.Tasks;
using Xunit;

namespace User.Api.Integration.Tests
{
    public class CreateUserTests : IntegrationTestsBase
    {
        [Fact(DisplayName = "When POST a new user, should get OK status code and user json")]
        public async Task CreateUser()
        {
            var email = $"{Guid.NewGuid()}@skitterbytes.com";
            var user = await CreateUserInternal(email, "GlobalAdmin", "Active");

            Assert.NotNull(user);
            Assert.Equal(email, user.Email);
            Assert.Equal("GlobalAdmin", user.Role);
            Assert.Equal("Active", user.Status);
        }
    }
}