using System;
using System.Net;
using System.Threading.Tasks;
using Flurl.Http;
using Xunit;

namespace User.Api.Integration.Tests
{
    public class DeleteUserTests : IntegrationTestsBase
    {
        [Fact(DisplayName = "When DELETE a user, should get OK status code")]
        public async Task DeleteUser()
        {
            // create a user
            var email = $"{Guid.NewGuid()}@skitterbytes.com";
            var createdUser = await CreateUserInternal(email, "User", "Inactive");

            // delete the user
            var response = await ApiUrl
                .AllowAnyHttpStatus()
                .AppendPathSegment(createdUser.UserId)
                .DeleteAsync();
            
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
            Assert.Null(await GetUserInternal(createdUser.UserId));
        }
    }
}