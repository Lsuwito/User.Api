using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using User.Api.Models;
using User.Api.Repositories.Entities;

namespace User.Api.Repositories
{
    /// <summary>
    /// Provide repository methods for User entity
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">An instance of <see cref="UserEntity"/>.</param>
        /// <returns>A user id.</returns>
        Task<Guid> CreateUserAsync(UserEntity user);

        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns>An instance of <see cref="UserEntity"/>.</returns>
        Task<UserEntity> GetUserAsync(Guid userId);

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="user">An instance of <see cref="UserEntity"/>.</param>
        /// <returns>A boolean value to indicate operation is successful or failed</returns>
        Task<bool> UpdateUserAsync(UserEntity user);

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns>A boolean value to indicate operation is successful or failed</returns>
        Task<bool> DeleteUserAsync(Guid userId);

        /// <summary>
        /// Get users.
        /// </summary>
        /// <param name="sortBy">Column to sort on.</param>
        /// <param name="sortAsc">Boolean indicator to sort in ascending direction.</param>
        /// <param name="limit">Limit number of records to return.</param>
        /// <param name="lastSortValue">Cursor starting point value.</param>
        /// <param name="lastSecondarySortValue">Cursor starting point secondary value.</param>
        /// <returns></returns>
        Task<IEnumerable<UserEntity>> GetUsersAsync(string sortBy, bool sortAsc, int limit, string lastSortValue,
            string lastSecondarySortValue); 
        
    }
}