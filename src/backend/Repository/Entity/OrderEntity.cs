using System;
using Repository.Enums;

namespace Repository.Entity
{
    public class OrderEntity : BaseEntity
    {
        public int UserId { get; set; }

        public string Text { get; set; }

        public decimal Price { get; set; }

        public UserEntity User { get; set; }

        public List<OrderProductEntity> OrdersProducts { get; set; }
    }
}
