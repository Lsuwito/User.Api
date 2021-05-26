using System;
using User.Api.Models;

namespace User.Api.Repositories.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public UserStatusEnum Status { get; set; }
    }
}