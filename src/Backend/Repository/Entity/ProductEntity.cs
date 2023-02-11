using System;
using Repository.Enums;

namespace Repository.Entity
{
    public class ProductEntity : BaseEntity
    {
        public char Symbol { get; set; }

        public decimal Price { get; set; }

        public decimal Sale { get; set; }

        public string Description { get; set; }

        public List<OrderProductEntity> OrdersProducts { get; set; }
    }
}
