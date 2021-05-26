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
        private readonly ICommandProvider _commandProvider;

        public UserRepository(IDataAccess dataAccess, ICommandProvider commandProvider)
        {
            _dataAccess = dataAccess;
            _commandProvider = commandProvider;
        }

        /// <inheritdoc />
        public async Task<Guid> CreateUserAsync(UserEntity user)
        {
            try
            {
                return await _dataAccess.ExecuteScalarAsync<Guid>(_commandProvider.GetCreateUserCommand(user));
            }
            catch (UniqueConstraintViolationException ex)
            {
                throw new ResourceConflictException($"User '{user.Email}' already exists.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<UserEntity> GetUserAsync(Guid userId)
        {
            return await _dataAccess.QuerySingleOrDefaultAsync<UserEntity>(_commandProvider.GetGetUserCommand(userId));
        }
    }
}