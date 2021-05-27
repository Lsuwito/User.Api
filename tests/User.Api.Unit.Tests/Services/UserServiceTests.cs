using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
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
        private readonly IPaginationCursorConverter _paginationCursorConverter;
        private readonly PaginationOptions _paginationOptions;

        public UserServiceTests()
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>())
                .CreateMapper();
            
            _userRepository = new Mock<IUserRepository>(MockBehavior.Strict).Object;
            _paginationCursorConverter = new Mock<IPaginationCursorConverter>(MockBehavior.Strict).Object;
            _paginationOptions = new PaginationOptions
            {
                BaseUrl = "http://localhost:5000"
            };
            
            var mockOptions = new Mock<IOptions<PaginationOptions>>();
            mockOptions.Setup(x => x.Value).Returns(_paginationOptions);
            
            _userService = new UserService(_userRepository, mapper, _paginationCursorConverter, mockOptions.Object);
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

        [Fact(DisplayName = "When get users, should return a list of users and next url")]
        public async Task GetUsers()
        {
            var mockUserRepository = Mock.Get(_userRepository);
            
            var entities = new List<UserEntity>
            {
                new()
                {
                    UserId = Guid.NewGuid(), 
                    Email = "user1@skitterbytes.com", 
                    Role = RoleEnum.Admin, 
                    Status = UserStatusEnum.Active
                },
                new()
                {
                    UserId = Guid.NewGuid(), 
                    Email = "user2@skitterbytes.com", 
                    Role = RoleEnum.User, 
                    Status = UserStatusEnum.Inactive
                }
            };
            
            mockUserRepository
                .Setup(x => x.GetUsersAsync("email", true, 10, null, null))
                .ReturnsAsync(entities);

            var mockCursorConverter = Mock.Get(_paginationCursorConverter);
            var cursorId = "xyz";
            
            mockCursorConverter
                .Setup(x => x.ToString(It.IsAny<PaginationCursor>()))
                .Returns(cursorId);
            
            var users = await _userService.GetUsersAsync(SortByEnum.Email, 10, null);

            Assert.Collection(users.Items, 
                user => AssertUser(entities[0], user), 
                user => AssertUser(entities[1], user));
            
            Assert.Equal($"{_paginationOptions.BaseUrl}/users?sortBy=Email&size=10&cursorId={cursorId}", users.NextUrl);
        }

        [Fact(DisplayName = "When get users with cursor, should get a list of users starting from the specified cursor")]
        public async Task GetUsersWithCursor()
        {
            var mockUserRepository = Mock.Get(_userRepository);
            
            var entities = new List<UserEntity>
            {
                new()
                {
                    UserId = Guid.NewGuid(), 
                    Email = "user1@skitterbytes.com", 
                    Role = RoleEnum.Admin, 
                    Status = UserStatusEnum.Active
                }
            };
            
            var mockCursorConverter = Mock.Get(_paginationCursorConverter);
            var cursorId = "xyz";
            var nextCursorId = "abc";
            var cursor = new PaginationCursor
            {
                LastSortValue = "email",
                LastSecondarySortValue = Guid.NewGuid().ToString()
            };
            
            mockCursorConverter
                .Setup(x => x.FromString(cursorId))
                .Returns(cursor);
            
            mockCursorConverter
                .Setup(x => x.ToString(It.IsAny<PaginationCursor>()))
                .Returns(nextCursorId);
            
            mockUserRepository
                .Setup(x => x.GetUsersAsync(
                    "email", true, 10, cursor.LastSortValue, cursor.LastSecondarySortValue))
                .ReturnsAsync(entities);
            
            var users = await _userService.GetUsersAsync(SortByEnum.Email, 10, cursorId);

            Assert.Collection(users.Items, user => AssertUser(entities[0], user));
            
            Assert.Equal($"{_paginationOptions.BaseUrl}/users?sortBy=Email&size=10&cursorId={nextCursorId}", users.NextUrl);
        }
        
        [Fact(DisplayName = "When get users and result is empty, should have no next url")]
        public async Task GetUsersEmptyResult()
        {
            var mockUserRepository = Mock.Get(_userRepository);
            var entities = Enumerable.Empty<UserEntity>();
            
            mockUserRepository
                .Setup(x => x.GetUsersAsync("email", true, 10, null, null))
                .ReturnsAsync(entities);
            
            var users = await _userService.GetUsersAsync(SortByEnum.Email, 10, null);

            Assert.Empty(users.Items);
            Assert.Null(users.NextUrl);
        }
        
        private void AssertUser(UserEntity expected, Models.User actual)
        {
            Assert.Equal(expected.UserId, actual.UserId);
            Assert.Equal(expected.Email, actual.Email);
            Assert.Equal(expected.Role, actual.Role);
            Assert.Equal(expected.Status, actual.Status);
        }
    }
}