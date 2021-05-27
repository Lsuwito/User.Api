using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Moq;
using User.Api.DataAccess;
using User.Api.Repositories;
using User.Api.Repositories.Entities;
using Xunit;

namespace User.Api.Unit.Tests.Repositories
{
    public class UserRepositoryTests
    {
        private readonly IDataAccess _dataAccess;
        private readonly ICommandDefinitionBuilder _commandDefinitionBuilder;
        private readonly UserRepository _userRepository;

        public UserRepositoryTests()
        {
            _dataAccess = new Mock<IDataAccess>().Object;
            _commandDefinitionBuilder = new Mock<ICommandDefinitionBuilder>().Object;
            _userRepository = new UserRepository(_dataAccess, _commandDefinitionBuilder);
        }

        [Fact(DisplayName = "When create a user, should create a command definition and call execute scalar")]
        public async Task CreateUser()
        {
            var mockCommandProvider = Mock.Get(_commandDefinitionBuilder);
            var entity = new UserEntity();
            var commandDefinition = new CommandDefinition();
            
            mockCommandProvider
                .Setup(x => x.BuildCreateUserCommand(entity))
                .Returns(commandDefinition);

            var mockDataAccess = Mock.Get(_dataAccess);
            var userId = Guid.NewGuid();

            mockDataAccess
                .Setup(x => x.ExecuteScalarAsync<Guid>(commandDefinition))
                .ReturnsAsync(userId);

            Assert.Equal(userId, await _userRepository.CreateUserAsync(entity));
        }
        
        [Fact(DisplayName = "When get a user, should create a command definition and call query single or default")]
        public async Task GetUser()
        {
            var mockCommandProvider = Mock.Get(_commandDefinitionBuilder);
            var userId = Guid.NewGuid();
            var commandDefinition = new CommandDefinition();
            
            mockCommandProvider
                .Setup(x => x.BuildGetUserCommand(userId))
                .Returns(commandDefinition);

            var mockDataAccess = Mock.Get(_dataAccess);
            var entity = new UserEntity();
            mockDataAccess
                .Setup(x => x.QuerySingleOrDefaultAsync<UserEntity>(commandDefinition))
                .ReturnsAsync(entity);

            Assert.Same(entity, await _userRepository.GetUserAsync(userId));
        }
        
        [Theory(DisplayName = "When update a user, should create a command definition and call execute scalar")]
        [InlineData(1)]
        [InlineData(0)]
        public async Task UpdateUser(int updatedCount)
        {
            var mockCommandProvider = Mock.Get(_commandDefinitionBuilder);
            var entity = new UserEntity();
            var commandDefinition = new CommandDefinition();
            
            mockCommandProvider
                .Setup(x => x.BuildUpdateUserCommand(entity))
                .Returns(commandDefinition);

            var mockDataAccess = Mock.Get(_dataAccess);
            
            mockDataAccess
                .Setup(x => x.ExecuteScalarAsync<int>(commandDefinition))
                .ReturnsAsync(updatedCount);
            
            Assert.Equal(updatedCount==1, await _userRepository.UpdateUserAsync(entity));
        }
        
        [Theory(DisplayName = "When delete a user, should create a command definition and call execute scalar")]
        [InlineData(1)]
        [InlineData(0)]
        public async Task DeleteUser(int deletedCount)
        {
            var mockCommandProvider = Mock.Get(_commandDefinitionBuilder);
            var userId = Guid.NewGuid();
            var commandDefinition = new CommandDefinition();
            
            mockCommandProvider
                .Setup(x => x.BuildDeleteUserCommand(userId))
                .Returns(commandDefinition);

            var mockDataAccess = Mock.Get(_dataAccess);
            
            mockDataAccess
                .Setup(x => x.ExecuteScalarAsync<int>(commandDefinition))
                .ReturnsAsync(deletedCount);
            
            Assert.Equal(deletedCount==1, await _userRepository.DeleteUserAsync(userId));
        }
        
        [Fact(DisplayName = "When get users, should create a command definition and call query")]
        public async Task GetUsers()
        {
            var mockCommandProvider = Mock.Get(_commandDefinitionBuilder);
            var commandDefinition = new CommandDefinition();
            
            mockCommandProvider
                .Setup(x => x.BuildGetUsersCommand("email", true, 10, "user1@test.com", "xyz"))
                .Returns(commandDefinition);

            var mockDataAccess = Mock.Get(_dataAccess);
            var entities = Enumerable.Empty<UserEntity>();
            mockDataAccess
                .Setup(x => x.QueryAsync<UserEntity>(commandDefinition))
                .ReturnsAsync(entities);

            Assert.Same(entities, await _userRepository
                .GetUsersAsync("email", true, 10, "user1@test.com", "xyz"));
        }
    }
}