using System;
using Repository.Enums;

namespace Service.Dto
{
    public class UserRequestDto
    {
        public string Username { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
