using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class UserAddressReadRepository : ReadRepository<UserAddress>, IUserAddressReadRepository
    {
        public UserAddressReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
