using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class BasketWriteRepository(ETicaretAPIDbContext context) : WriteRepository<Basket>(context), IBasketWriteRepository
    {

    }
}
