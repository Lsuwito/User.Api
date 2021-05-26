using System;
using System.Data;
using Dapper;
using User.Api.DataAccess;
using User.Api.Models;
using User.Api.Repositories.Entities;
using Xunit;

namespace User.Api.Tests.DataAccess
{
    public class PostgresCommandDefinitionBuilderTests
    {
        private readonly PostgresCommandDefinitionBuilder _commandDefinitionBuilder;

        public PostgresCommandDefinitionBuilderTests()
        {
            _commandDefinitionBuilder = new PostgresCommandDefinitionBuilder();
        }

        [Fact(DisplayName = "Build command definition for create user operation")]
        public void BuildCreateUserCommandDefinition()
        {
            var entity = new UserEntity
            {
                Email = "qa@skitterbytes.com",
                Role = RoleEnum.Admin,
                Status = UserStatusEnum.Active
            };

            var commandDefinition = _commandDefinitionBuilder.BuildCreateUserCommand(entity);
            Assert.Equal("users.create_user", commandDefinition.CommandText);
            Assert.Equal(CommandType.StoredProcedure, commandDefinition.CommandType);
            
            var parameters = Assert.IsType<DynamicParameters>(commandDefinition.Parameters);
            
            AssertParameter(parameters, "email_in", entity.Email);
            AssertParameter(parameters, "role_in", entity.Role);
            AssertParameter(parameters, "status_in", entity.Status);
        }
        
        [Fact(DisplayName = "Build command definition for update user operation")]
        public void BuildUpdateUserCommandDefinition()
        {
            var entity = new UserEntity
            {
                UserId = Guid.NewGuid(),
                Email = "qa@skitterbytes.com",
                Role = RoleEnum.Admin,
                Status = UserStatusEnum.Active
            };

            var commandDefinition = _commandDefinitionBuilder.BuildUpdateUserCommand(entity);
            Assert.Equal("users.update_user", commandDefinition.CommandText);
            Assert.Equal(CommandType.StoredProcedure, commandDefinition.CommandType);
            
            var parameters = Assert.IsType<DynamicParameters>(commandDefinition.Parameters);
            
            AssertParameter(parameters, "user_id_in", entity.UserId);
            AssertParameter(parameters, "role_in", entity.Role);
            AssertParameter(parameters, "status_in", entity.Status);
        }
        
        [Fact(DisplayName = "Build command definition for delete user operation")]
        public void BuildDeleteUserCommandDefinition()
        {
            var userId = Guid.NewGuid();
            
            var commandDefinition = _commandDefinitionBuilder.BuildDeleteUserCommand(userId);
            Assert.Equal("users.delete_user", commandDefinition.CommandText);
            Assert.Equal(CommandType.StoredProcedure, commandDefinition.CommandType);
            
            var parameters = Assert.IsType<DynamicParameters>(commandDefinition.Parameters);
            
            AssertParameter(parameters, "user_id_in", userId);
        }

        [Fact(DisplayName = "Build command definition for get user operation")]
        public void BuildGetUserCommandDefinition()
        {
            var userId = Guid.NewGuid();
            
            var commandDefinition = _commandDefinitionBuilder.BuildGetUserCommand(userId);
            Assert.Equal("users.get_user", commandDefinition.CommandText);
            Assert.Equal(CommandType.StoredProcedure, commandDefinition.CommandType);
            
            var parameters = Assert.IsType<DynamicParameters>(commandDefinition.Parameters);
            
            AssertParameter(parameters, "user_id_in", userId);
        }
        
        private void AssertParameter<T>(DynamicParameters parameters, string name, T expectedValue)
        {
            Assert.Contains(parameters.ParameterNames, x => x == name);
            Assert.Equal(expectedValue, parameters.Get<T>(name));
        }
    }
}