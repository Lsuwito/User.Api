using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using User.Api.Exceptions;
using User.Api.Models;
using User.Api.Services;

namespace User.Api.Controllers
{
    /// <summary>
    /// Provide controller methods to manage users.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        
        /// <summary>
        /// Get a list of users.
        /// </summary>
        /// <param name="size">Maximum number of users to return in the response.</param>
        /// <param name="sortBy">Sort the user list according to the specified field.</param>
        /// <param name="cursorId">Cursor reference to get the next batch of result.</param>
        /// <returns>An instance of <see cref="OkObjectResult"/> with a <see cref="Users"/> object as the value,
        /// or an <see cref="Error"/> object if operation fails.</returns>
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] int? size, [FromQuery] SortByEnum? sortBy, [FromQuery] string cursorId)
        {
            var users = await _userService.GetUsersAsync(sortBy ?? SortByEnum.Email, size ?? 10, cursorId);
            return Ok(users);
        }
        
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request">An instance of <see cref="UserRequest"/> containing user data.</param>
        /// <returns>An instance of <see cref="OkObjectResult"/> with a <see cref="User"/> object as the value,
        /// or an <see cref="Error"/> object if operation fails.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody, Required] UserRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            return Ok(user);
        }
        
        /// <summary>
        /// Get a user by ID.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns>An instance of <see cref="OkObjectResult"/> with a <see cref="User"/> object as the value,
        /// or an <see cref="Error"/> object if operation fails.</returns>
        /// <exception cref="ResourceNotFoundException"></exception>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid userId)
        {
            var user = await _userService.GetUserAsync(userId);
            return Ok(user);
        }
        
        /// <summary>
        /// Update a user.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <param name="request">An instance of <see cref="UserRequest"/>.</param>
        /// <returns>An instance of <see cref="OkObjectResult"/> with a <see cref="User"/> object as the value,
        /// or an <see cref="Error"/> object if operation fails.</returns>
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid userId, [FromBody, Required] UserRequest request)
        {
            var user = await _userService.UpdateUserAsync(userId, request);
            return Ok(user);
        }
        
        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        /// <param name="userId">A user ID.</param>
        /// <returns>
        /// An instance of <see cref="OkResult"/>, or an <see cref="Error"/> object if operation fails.
        /// </returns>
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }
    }
}