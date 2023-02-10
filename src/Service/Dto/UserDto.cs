using System;
using Repository.Enums;

namespace Service.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }
    }
}
