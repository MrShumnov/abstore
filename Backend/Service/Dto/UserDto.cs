using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using Repository.Enums;

namespace Service.Dto
{
    public class UserDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }
    }
}
