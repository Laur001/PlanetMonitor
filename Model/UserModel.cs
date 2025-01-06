using Cosmos.Data;
using System;

namespace Cosmos.Model
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public RoleModel Role { get; set; }


    }
}
