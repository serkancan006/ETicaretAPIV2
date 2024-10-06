using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class BasketItemWriteRepository(ETicaretAPIDbContext context) : WriteRepository<BasketItem>(context), IBasketItemWriteRepository
    {

    }
}
