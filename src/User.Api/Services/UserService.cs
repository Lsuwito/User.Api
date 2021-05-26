using System;
using System.Threading.Tasks;
using AutoMapper;
using User.Api.Exceptions;
using User.Api.Models;
using User.Api.Repositories;
using User.Api.Repositories.Entities;

namespace User.Api.Services
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
    
        /// <inheritdoc />
        public async Task<Models.User> CreateUserAsync(UserRequest userRequest)
        {
            var entity = _mapper.Map<UserEntity>(userRequest);
            entity.UserId = await _userRepository.CreateUserAsync(entity);
            return _mapper.Map<Models.User>(entity);
        }

        /// <inheritdoc />
        public async Task<Models.User> GetUserAsync(Guid userId)
        {
            var entity = await _userRepository.GetUserAsync(userId);
            
            if (entity == null)
            {
                throw new ResourceNotFoundException("User was not found.");
            }
            
            return _mapper.Map<Models.User>(entity);
        }
        
        /// <inheritdoc />
        public async Task DeleteUserAsync(Guid userId)
        {
            if (!await _userRepository.DeleteUserAsync(userId))
            {
                throw new ResourceNotFoundException("Users was not found.");
            }
        }
        
        /// <inheritdoc />
        public async Task<Models.User> UpdateUserAsync(Guid userId, UserRequest userRequest)
        {
            var entity = _mapper.Map<UserEntity>(userRequest);
            entity.UserId = userId;
            
            if (!await _userRepository.UpdateUserAsync(entity))
            {
                throw new ResourceNotFoundException("Users was not found.");
            }
            
            return _mapper.Map<Models.User>(entity);
        }
    }
}