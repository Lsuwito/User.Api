using System;
using System.Threading.Tasks;
using AutoMapper;
using User.Api.Models;
using User.Api.Repositories;
using User.Api.Repositories.Entities;

namespace User.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        
        public async Task<Models.User> CreateUserAsync(UserRequest userRequest)
        {
            var entity = _mapper.Map<UserEntity>(userRequest);
            entity.UserId = await _userRepository.CreateUserAsync(entity);
            return _mapper.Map<Models.User>(entity);
        }

        public async Task<Models.User> GetUserAsync(Guid userId)
        {
            var entity = await _userRepository.GetUserAsync(userId);
            return _mapper.Map<Models.User>(entity);
        }
    }
}