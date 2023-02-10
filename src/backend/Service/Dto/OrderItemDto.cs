using System;
using Repository.Enums;

namespace Service.Dto
{
    public class OrderItemDto
    {
        public ProductDto Product { get; set; }

        public int Qty { get; set; }
    }
}
