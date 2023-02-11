using Repository.IRepository;
using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repository.Context;

namespace Repository.Repository
{
    public class OrdersProductsRepository : BaseRepository<OrderProductEntity>, IOrdersProductsRepository
    {
        public OrdersProductsRepository(BaseContext context) : base(context)
        {

        }
    }
}
