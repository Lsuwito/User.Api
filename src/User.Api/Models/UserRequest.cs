namespace User.Api.Models
{
    public class UserRequest
    {
        public string Email { get; set; }
        public UserStatusEnum Status { get; set; }
        public RoleEnum Role { get; set; }
    }
}