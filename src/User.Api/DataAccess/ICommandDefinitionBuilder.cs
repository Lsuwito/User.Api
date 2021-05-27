using System;
using Dapper;
using User.Api.Models;
using User.Api.Repositories.Entities;

namespace User.Api.DataAccess
{
    /// <summary>
    /// Provide methods to build data access command definition for user CRUD operations.
    /// </summary>
    public interface ICommandDefinitionBuilder
    {
        /// <summary>
        /// Build a command definition for create user operation.
        /// </summary>
        /// <param name="user">An instance of <see cref="UserEntity"/>.</param>
        /// <returns>An instance of <see cref="CommandDefinition"/>.</returns>
        CommandDefinition BuildCreateUserCommand(UserEntity user);

        /// <summary>
        /// Build a command definition for get user operation.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns>An instance of <see cref="CommandDefinition"/>.</returns>
        CommandDefinition BuildGetUserCommand(Guid userId);

        /// <summary>
        /// Build a command definition for delete user operation.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns>An instance of <see cref="CommandDefinition"/>.</returns>
        CommandDefinition BuildDeleteUserCommand(Guid userId);

        /// <summary>
        /// Build a command definition for update user operation.
        /// </summary>
        /// <param name="user">A user ID.</param>
        /// <returns>An instance of <see cref="CommandDefinition"/>.</returns>
        CommandDefinition BuildUpdateUserCommand(UserEntity user);

        /// <summary>
        /// Build a command definition for get users
        /// </summary>
        /// <param name="sortBy">Column to sort on.</param>
        /// <param name="sortAsc">Boolean indicator to sort in ascending direction.</param>
        /// <param name="limit">Limit number of records to return.</param>
        /// <param name="lastSortValue">Cursor starting point value.</param>
        /// <param name="lastSecondarySortValue">Cursor starting point secondary value.</param>
        /// <returns></returns>
        CommandDefinition BuildGetUsersCommand(string sortBy, bool sortAsc, int limit, 
            string lastSortValue, string lastSecondarySortValue);
    }
}