using System;
using Microsoft.AspNetCore.Mvc;
using User.Api.Models;

namespace User.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers([FromQuery] int? size, [FromQuery] SortByEnum? sortBy, [FromQuery] string cursorId)
        {
            throw new NotImplementedException();
        }
        
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequest request)
        {
            throw new NotImplementedException();
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