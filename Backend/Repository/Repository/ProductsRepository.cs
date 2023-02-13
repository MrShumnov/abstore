using Repository.IRepository;
using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repository.Context;

namespace Repository.Repository
{
    public class ProductsRepository : BaseRepository<ProductEntity>, IProductsRepository
    {
        public ProductsRepository(BaseContext context) : base(context)
        {

        }
    }
}
