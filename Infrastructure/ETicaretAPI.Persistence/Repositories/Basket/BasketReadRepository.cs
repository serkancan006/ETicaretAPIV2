using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class BasketReadRepository(ETicaretAPIDbContext context) : ReadRepository<Basket>(context), IBasketReadRepository
    {

    }
}
