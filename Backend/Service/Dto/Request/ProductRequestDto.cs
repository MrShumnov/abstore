using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace Service.Dto
{
    [DataContract]
    public class ProductRequestDto
    {
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
