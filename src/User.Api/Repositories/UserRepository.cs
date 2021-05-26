using System;
using System.Threading.Tasks;
using User.Api.DataAccess;
using User.Api.Exceptions;
using User.Api.Repositories.Entities;

namespace User.Api.Repositories
{
    /// <inheritdoc />
    public class UserRepository : IUserRepository
    {
        private readonly IDataAccess _dataAccess;
        private readonly ICommandDefinitionBuilder _commandDefinitionBuilder;

        public UserRepository(IDataAccess dataAccess, ICommandDefinitionBuilder commandDefinitionBuilder)
        {
            _dataAccess = dataAccess;
            _commandDefinitionBuilder = commandDefinitionBuilder;
        }

        /// <inheritdoc />
        public async Task<Guid> CreateUserAsync(UserEntity user)
        {
            try
            {
                return await _dataAccess.ExecuteScalarAsync<Guid>(_commandDefinitionBuilder.BuildCreateUserCommand(user));
            }
            catch (UniqueConstraintViolationException ex)
            {
                throw new ResourceConflictException($"User '{user.Email}' already exists.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<UserEntity> GetUserAsync(Guid userId)
        {
            return await _dataAccess.QuerySingleOrDefaultAsync<UserEntity>(_commandDefinitionBuilder.BuildGetUserCommand(userId));
        }
        
        /// <inheritdoc />
        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            var count = await _dataAccess.ExecuteScalarAsync<int>(_commandDefinitionBuilder.BuildDeleteUserCommand(userId));
            return count == 1;
        }
        
        /// <inheritdoc />
        public async Task<bool> UpdateUserAsync(UserEntity user)
        {
            var count = await _dataAccess.ExecuteScalarAsync<int>(_commandDefinitionBuilder.BuildUpdateUserCommand(user));
            return count == 1;
        }
    }
}