using System;
using System.Runtime.Serialization;

namespace Service.Dto
{
    [DataContract]
    public class ProductDto
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "symbol")]
        public char Symbol { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        [DataMember(Name = "sale")]
        public decimal Sale { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
