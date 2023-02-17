using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Repository.Enums;

namespace Service.Dto
{
    [DataContract]
    public class UserAdminDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "login")]
        public string Login { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "role")]
        public RoleEnum Role { get; set; }
    }
}
