using System;
using System.Runtime.Serialization;
using Repository.Enums;

namespace Service.Dto
{
    [DataContract]
    public class OrderItemDto
    {
        [DataMember(Name = "product")]
        public ProductDto Product { get; set; }

        [DataMember(Name = "qty")]
        public int Qty { get; set; }
    }
}
