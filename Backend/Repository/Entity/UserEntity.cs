using System;
using Repository.Enums;

namespace Repository.Entity
{
    public class UserEntity : BaseEntity
    {
        public string Username { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public RoleEnum Role { get; set; }

        public List<OrderEntity> Orders { get; set; }
    }
}
