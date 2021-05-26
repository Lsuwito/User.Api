using System;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using User.Api.Exceptions;
using User.Api.Models;
using User.Api.Repositories;
using User.Api.Repositories.Entities;
using User.Api.Services;
using Xunit;

namespace User.Api.Unit.Tests.Services
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                .CreateMapper();
            
            _userRepository = new Mock<IUserRepository>().Object;
            _userService = new UserService(_userRepository, mapper);
        }

        [Fact(DisplayName = "When create a user, should create user in repository and return a user model")]
        public async Task CreateUser()
        {
            var request = new UserRequest
            {
                Email = "qa@skitterbytes.com",
                Role = RoleEnum.Admin,
                Status = UserStatusEnum.Active
            };

            var mockUserRepository = Mock.Get(_userRepository);
            var userId = Guid.NewGuid();
            
            mockUserRepository
                .Setup(x => x.CreateUserAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(userId);
            
            var user = await _userService.CreateUserAsync(request);
            
            Assert.Equal(userId, user.UserId);
            Assert.Equal(request.Email, user.Email);
            Assert.Equal(request.Role, user.Role);
            Assert.Equal(request.Status, user.Status);
        }
        
        [Fact(DisplayName = "When get a user, should get the user entity from repository and return a user model")]
        public async Task GetUser()
        {
            var entity = new UserEntity
            {
                UserId = Guid.NewGuid(),
                Email = "qa@skitterbytes.com",
                Role = RoleEnum.Admin,
                Status = UserStatusEnum.Active
            };

            var mockUserRepository = Mock.Get(_userRepository);

            mockUserRepository
                .Setup(x => x.GetUserAsync(entity.UserId))
                .ReturnsAsync(entity);
            
            var user = await _userService.GetUserAsync(entity.UserId);
            
            Assert.Equal(entity.UserId, user.UserId);
            Assert.Equal(entity.Email, user.Email);
            Assert.Equal(entity.Role, user.Role);
            Assert.Equal(entity.Status, user.Status);
        }

        [Fact(DisplayName = "When delete a user, should delete the user entity from repository")]
        public async Task DeleteUser()
        {
            var userId = Guid.NewGuid();
            var mockUserRepository = Mock.Get(_userRepository);

            mockUserRepository
                .Setup(x => x.DeleteUserAsync(userId))
                .ReturnsAsync(true);

            await _userService.DeleteUserAsync(userId);
        }
        
        [Fact(DisplayName = "When delete a user and not found, should throw resource not found exception")]
        public async Task DeleteUserNotFound()
        {
            var userId = Guid.NewGuid();
            var mockUserRepository = Mock.Get(_userRepository);

            mockUserRepository
                .Setup(x => x.DeleteUserAsync(userId))
                .ReturnsAsync(false);
            
            await Assert.ThrowsAsync<ResourceNotFoundException>(() => _userService.DeleteUserAsync(userId));
        }
        
        [Fact(DisplayName = "When update a user, should update user in the repository and return a user model")]
        public async Task UpdateUser()
        {
            var request = new UserRequest
            {
                Email = "qa@skitterbytes.com",
                Role = RoleEnum.Admin,
                Status = UserStatusEnum.Active
            };

            var mockUserRepository = Mock.Get(_userRepository);
            mockUserRepository
                .Setup(x => x.UpdateUserAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(true);
            
            var userId = Guid.NewGuid();
            var user = await _userService.UpdateUserAsync(userId, request);
            
            Assert.Equal(userId, user.UserId);
            Assert.Equal(request.Email, user.Email);
            Assert.Equal(request.Role, user.Role);
            Assert.Equal(request.Status, user.Status);
        }
        
        [Fact(DisplayName = "When update a user and not found, should throw resource not found exception")]
        public async Task UpdateUserNotFound()
        {
            var request = new UserRequest
            {
                Email = "qa@skitterbytes.com",
                Role = RoleEnum.Admin,
                Status = UserStatusEnum.Active
            };

            var mockUserRepository = Mock.Get(_userRepository);
            mockUserRepository
                .Setup(x => x.UpdateUserAsync(It.IsAny<UserEntity>()))
                .ReturnsAsync(false);
            
            var userId = Guid.NewGuid();
            await Assert.ThrowsAsync<ResourceNotFoundException>(() => _userService.UpdateUserAsync(userId, request));
        }
    }
}