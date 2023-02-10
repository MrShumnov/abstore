using System;
using Repository.Enums;

namespace Service.Dto
{
    public class OrderRequestDto
    {
        public int UserId { get; set; }

        public List<ProductDto> Items { get; set; }
    }
}
