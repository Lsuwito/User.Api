using Dapper;
using User.Api.Repositories.Entities;

namespace User.Api.DataAccess
{
    /// <summary>
    /// Provide methods to create data access command definition for user CRUD operations.
    /// </summary>
    public interface ICommandProvider
    {
        /// <summary>
        /// Build a command definition for create user operation.
        /// </summary>
        /// <param name="user">An instance of <see cref="UserEntity"/>.</param>
        /// <returns>An instance of <see cref="CommandDefinition"/>.</returns>
        CommandDefinition GetCreateUserCommand(UserEntity user);
    }
}