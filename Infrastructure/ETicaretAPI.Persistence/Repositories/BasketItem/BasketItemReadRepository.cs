using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class BasketItemReadRepository(ETicaretAPIDbContext context) : ReadRepository<BasketItem>(context), IBasketItemReadRepository
    {

    }
}
