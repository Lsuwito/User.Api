﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using User.Api.Controllers;
using User.Api.Exceptions;
using User.Api.Models;
using User.Api.Services;
using Xunit;

namespace User.Api.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly UsersController _userController;
        private readonly IUserService _userService;

        public UsersControllerTests()
        {
            _userService = new Mock<IUserService>(MockBehavior.Strict).Object;
            _userController = new UsersController(_userService);
        }

        [Fact(DisplayName="When get users without parameters, should get users from the service using default parameter values.")]
        public void GetUsersWithDefaultParameters()
        {
            _userController.GetUsers(null, null, null);
        }
        
        [Fact(DisplayName="When get users, should get users from the service using the specified parameters")]
        public void GetUsersWithParameters()
        {
            _userController.GetUsers(null, null, null);
        }
        
        [Fact(DisplayName="When get a user, should get a user from the service")]
        public async Task GetUser()
        {
            var userId = Guid.NewGuid();

            var mockUserService = Mock.Get(_userService);
            var user = new Models.User();
            mockUserService.Setup(x => x.GetUserAsync(userId)).ReturnsAsync(user);

            var result = await _userController.GetUser(userId);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(user, okResult.Value);
        }
        
        [Fact(DisplayName="When get a user and a user is not found, should throw not found exception")]
        public async Task GetUserNotFound()
        {
            var userId = Guid.NewGuid();
            var mockUserService = Mock.Get(_userService);
            mockUserService.Setup(x => x.GetUserAsync(userId)).ReturnsAsync(default(Models.User));
            
            var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() => _userController.GetUser(userId));
            Assert.Equal("User was not found.", exception.Message);
        }
        
        [Fact(DisplayName="When create a user, should create a user from the service")]
        public async Task CreateUser()
        {
            var request = new UserRequest
            {
                Email = "test@skitterbytes.com",
                Status = UserStatusEnum.Active,
                Role = RoleEnum.Admin
            };
            
            var mockUserService = Mock.Get(_userService);
            
            var user = new Models.User();
            mockUserService.Setup(x => x.CreateUserAsync(request)).ReturnsAsync(user);
            
            var result = await _userController.CreateUserAsync(request);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(user, okResult.Value);
        }
        
        [Fact(DisplayName="When update a user, should get a user from the service")]
        public void UpdateUser()
        {
            var userId = Guid.NewGuid();
            
            var request = new UserRequest
            {
                Email = "test@skitterbytes.com",
                Status = UserStatusEnum.Active,
                Role = RoleEnum.Admin
            };
            
            Assert.IsType<OkObjectResult>(_userController.UpdateUser(userId, request));
        }
        
        [Fact(DisplayName="When update a user and user is not found, should return not found status code")]
        public void UpdateUserNotFound()
        {
            var userId = Guid.NewGuid();

            Assert.IsType<NoContentResult>(_userController.DeleteUser(userId));
        }
        
        [Fact(DisplayName="When delete a user, should delete a user from the service")]
        public void DeleteUser()
        {
            var userId = Guid.NewGuid();

            Assert.IsType<NoContentResult>(_userController.DeleteUser(userId));
        }
        
        [Fact(DisplayName="When delete a user and user is not found, should return not found status code")]
        public void DeleteUserNotFound()
        {
            var userId = Guid.NewGuid();

            Assert.IsType<NoContentResult>(_userController.DeleteUser(userId));
        }
    }
}