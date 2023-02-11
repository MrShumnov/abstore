using System;
using Repository.Enums;

namespace Service.Dto
{
    public class OrderDto
    {
        public int? Id { get; set; }

        public int UserId { get; set; }

        public string Text { get; set; }

        public decimal Price { get; set; }

        public DateTime Date { get; set; }
    }
}
