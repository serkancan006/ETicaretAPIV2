using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class UserAddressWriteRepository : WriteRepository<UserAddress>, IUserAddressWriteRepository
    {
        public UserAddressWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
