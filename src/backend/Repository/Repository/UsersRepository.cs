using Repository.IRepository;
using Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Repository.Context;

namespace Repository.Repository
{
    public class UsersRepository : BaseRepository<UserEntity>, IUsersRepository
    {
        public UsersRepository(BaseContext context) : base(context)
        {

        }
    }
}
