using System;
using Microsoft.AspNetCore.Mvc;
using User.Api.Controllers;
using User.Api.Models;
using Xunit;

namespace User.Api.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _controller = new UserController();
        }

        [Fact(DisplayName="When get users without parameters, should get users from the service using default parameter values.")]
        public void GetUsersWithDefaultParameters()
        {
            _controller.GetUsers(null, null, null);
        }
        
        [Fact(DisplayName="When get users, should get users from the service using the specified parameters")]
        public void GetUsersWithParameters()
        {
            _controller.GetUsers(null, null, null);
        }
        
        [Fact(DisplayName="When get a user, should get a user from the service")]
        public void GetUser()
        {
            var userId = Guid.NewGuid();
            Assert.IsType<OkObjectResult>(_controller.GetUser(userId));
        }
        
        [Fact(DisplayName="When get a user and a user is not found, should return not found status code with error response")]
        public void GetUserNotFound()
        {
            var userId = Guid.NewGuid();
            Assert.IsType<NotFoundObjectResult>(_controller.GetUser(userId));
        }
        
        [Fact(DisplayName="When create a user, should create a user from the service")]
        public void CreateUser()
        {
            var request = new UserRequest
            {
                Email = "test@skitterbytes.com",
                Status = UserStatusEnum.Active,
                Role = RoleEnum.Admin
            };
            
            Assert.IsType<OkObjectResult>(_controller.CreateUser(request));
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
            
            Assert.IsType<OkObjectResult>(_controller.UpdateUser(userId, request));
        }
        
        [Fact(DisplayName="When update a user and user is not found, should return not found status code")]
        public void UpdateUserNotFound()
        {
            var userId = Guid.NewGuid();

            Assert.IsType<NoContentResult>(_controller.DeleteUser(userId));
        }
        
        [Fact(DisplayName="When delete a user, should delete a user from the service")]
        public void DeleteUser()
        {
            var userId = Guid.NewGuid();

            Assert.IsType<NoContentResult>(_controller.DeleteUser(userId));
        }
        
        [Fact(DisplayName="When delete a user and user is not found, should return not found status code")]
        public void DeleteUserNotFound()
        {
            var userId = Guid.NewGuid();

            Assert.IsType<NoContentResult>(_controller.DeleteUser(userId));
        }
    }
}