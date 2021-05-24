using System;

namespace User.Api.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
        public RoleEnum Role { get; set; }
    }
}