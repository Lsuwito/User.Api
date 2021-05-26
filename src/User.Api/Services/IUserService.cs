using System.Threading.Tasks;
using User.Api.Models;

namespace User.Api.Services
{
    /// <summary>
    /// Provide methods for user management
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="userRequest">The <see cref="UserRequest"/> to be created.</param>
        /// <returns>An instance of <see cref="Models.User"/> containing the newly created user data.</returns>
        Task<Models.User> CreateUserAsync(UserRequest userRequest);
    }
}