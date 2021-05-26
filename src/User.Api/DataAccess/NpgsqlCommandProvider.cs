using System;
using System.Data;
using Dapper;
using User.Api.Repositories.Entities;

namespace User.Api.DataAccess
{
    public class NpgsqlCommandProvider : ICommandProvider
    {
        public CommandDefinition GetCreateUserCommand(UserEntity user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("email_in", user.Email, DbType.StringFixedLength);
            parameters.Add("role_in", user.Role);
            parameters.Add("status_in", user.Status);

            return new CommandDefinition("users.create_user", parameters, commandType: CommandType.StoredProcedure);
        }

        public CommandDefinition GetGetUserCommand(Guid userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("user_id_in", userId, DbType.Guid);
            
            return new CommandDefinition("users.get_user", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}