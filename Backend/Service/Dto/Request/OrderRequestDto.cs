using System;
using System.Runtime.Serialization;
using Repository.Enums;

namespace Service.Dto
{
    [DataContract]
    public class OrderRequestDto
    {
        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "items_ids")]
        public List<int> ItemsIds { get; set; }
    }
}
