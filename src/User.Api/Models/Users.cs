using System.Collections.Generic;

namespace User.Api.Models
{
    public class Users
    {
        public IEnumerable<User> Items { get; set; }
        public string NextUrl { get; set; }
    }
}