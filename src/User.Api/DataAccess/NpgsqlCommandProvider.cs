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
            parameters.Add("email", user.Email, DbType.StringFixedLength);
            parameters.Add("role", user.Role);
            parameters.Add("status", user.Status);

            return new CommandDefinition("users.create_user", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}