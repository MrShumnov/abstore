using System;

namespace Service.Dto
{
    public class ProductRequestDto
    {
        public char Symbol { get; set; }

        public decimal Price { get; set; }

        public decimal Sale { get; set; }

        public string Description { get; set; }
    }
}
