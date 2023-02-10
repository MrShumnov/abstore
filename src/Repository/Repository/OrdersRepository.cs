using Repository.IRepository;
using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repository.Context;

namespace Repository.Repository
{
    public class OrdersRepository : BaseRepository<OrderEntity>, IOrdersRepository
    {
        public OrdersRepository(BaseContext context) : base(context)
        {

        }
    }
}
