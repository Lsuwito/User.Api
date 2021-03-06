using System;
using System.Data;
using Dapper;
using User.Api.Models;
using User.Api.Repositories.Entities;

namespace User.Api.DataAccess
{
    /// <inheritdoc />
    public class PostgresCommandDefinitionBuilder : ICommandDefinitionBuilder
    {
        /// <inheritdoc />
        public CommandDefinition BuildCreateUserCommand(UserEntity user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("email_in", user.Email, DbType.StringFixedLength);
            parameters.Add("role_in", user.Role);
            parameters.Add("status_in", user.Status);

            return new CommandDefinition("users.create_user", parameters, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc />
        public CommandDefinition BuildGetUserCommand(Guid userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id_in", userId, DbType.Guid);
            
            return new CommandDefinition("users.get_user", parameters, commandType: CommandType.StoredProcedure);
        }
        
        /// <inheritdoc />
        public CommandDefinition BuildDeleteUserCommand(Guid userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id_in", userId, DbType.Guid);
            
            return new CommandDefinition("users.delete_user", parameters, commandType: CommandType.StoredProcedure);
        }
        
        /// <inheritdoc />
        public CommandDefinition BuildUpdateUserCommand(UserEntity user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id_in", user.UserId, DbType.Guid);
            parameters.Add("role_in", user.Role);
            parameters.Add("status_in", user.Status);
            
            return new CommandDefinition("users.update_user", parameters, commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc />
        public CommandDefinition BuildGetUsersCommand(string sortBy, bool sortAsc, int limit, string lastSortValue,
            string lastSecondarySortValue)
        {
            var parameters = new DynamicParameters();
            parameters.Add("sort_by_in", sortBy, DbType.String);
            parameters.Add("sort_ascending_in", sortAsc, DbType.Boolean);
            parameters.Add("limit_in", limit, DbType.Int32);
            parameters.Add("last_sort_value_in", lastSortValue, DbType.String);
            parameters.Add("last_secondary_sort_value_in", lastSecondarySortValue, DbType.String);
            
            return new CommandDefinition("users.get_users", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}