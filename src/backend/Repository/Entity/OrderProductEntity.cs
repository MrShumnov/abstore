using System;
using Repository.Enums;

namespace Repository.Entity
{
    public class OrderProductEntity : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        public OrderEntity Order { get; set; }

        public ProductEntity Product { get; set; }
    }
}
