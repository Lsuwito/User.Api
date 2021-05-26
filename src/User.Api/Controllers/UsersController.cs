using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        /// <returns>An action result with OK code with a <see cref="Users"/> object on successful query.
        /// Or an <see cref="Error"/> object on failure.</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public IActionResult GetUsers([FromQuery] int? size, [FromQuery] SortByEnum? sortBy, [FromQuery] string cursorId)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="request">An instance of <see cref="UserRequest"/> containing user data.</param>
        /// <returns>An action result with OK code with a <see cref="User"/> object on successful creation.
        /// Or an <see cref="Error"/> object on failure.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            return Ok(user);
        }
        
        [HttpGet("{userId}")]
        public IActionResult GetUser([FromRoute] Guid userId)
        {
            throw new NotImplementedException();
        }
        
        [HttpPut("{userId}")]
        public IActionResult UpdateUser([FromRoute] Guid userId, [FromBody] UserRequest request)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser([FromRoute] Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}