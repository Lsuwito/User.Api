using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Flurl.Http;
using Xunit;

namespace User.Api.Integration.Tests
{
    public abstract class IntegrationTestsBase
    {
        protected string ApiUrl = "http://api:5000/users";
        
        private readonly JsonSerializerOptions _serializationOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = {new JsonStringEnumConverter()}
        };
        
        protected async Task<Models.User> CreateUserInternal(string email, string role, string status)
        {
            var response = await ApiUrl
                .AllowAnyHttpStatus()
                .PostJsonAsync(new { email, role, status});
            
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);

            return await response.ResponseMessage.Content.ReadFromJsonAsync<Models.User>(_serializationOptions);
        }

        protected async Task<Api.Integration.Tests.Models.User> GetUserInternal(Guid userId)
        {
            var response = await ApiUrl
                .AllowAnyHttpStatus()
                .AppendPathSegment(userId)
                .GetAsync();

            if (response.StatusCode == (int) HttpStatusCode.NotFound)
                return null;

            return await response.ResponseMessage.Content.ReadFromJsonAsync<Models.User>(_serializationOptions);
        }
    }
}