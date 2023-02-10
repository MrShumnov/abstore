using System;

namespace Service.Dto
{
    public class ProductDto 
    {
        public int Id { get; set; }

        public char Symbol { get; set; }

        public decimal Price { get; set; }

        public decimal Sale { get; set; }

        public string Description { get; set; }
    }
}
