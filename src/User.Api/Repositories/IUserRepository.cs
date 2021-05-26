using System;
using System.Threading.Tasks;
using User.Api.Repositories.Entities;

namespace User.Api.Repositories
{
    /// <summary>
    /// Provide repository methods for User entity
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create a user.
        /// </summary>
        /// <param name="user">An instance of <see cref="UserEntity"/>.</param>
        /// <returns>A user id.</returns>
        Task<Guid> CreateUserAsync(UserEntity user);
    }
}