using System;
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

        /// <summary>
        /// Get a user by user ID.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns>An instance of <see cref="Models.User"/>.</returns>
        Task<Models.User> GetUserAsync(Guid userId);

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns></returns>
        Task DeleteUserAsync(Guid userId);

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <param name="userRequest">An instance of <see cref="UserRequest"/></param>
        /// <returns>An instance of <see cref="Models.User"/>.</returns>
        Task<Models.User> UpdateUserAsync(Guid userId, UserRequest userRequest);
    }
}