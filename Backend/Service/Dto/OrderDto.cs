using System;
using System.Runtime.Serialization;
using Repository.Enums;

namespace Service.Dto
{
    [DataContract]
    public class OrderDto
    {
        [DataMember(Name = "id")]
        public int? Id { get; set; }

        [DataMember(Name = "user_id")]
        public int UserId { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "date")]
        public DateTime Date { get; set; }
    }
}
