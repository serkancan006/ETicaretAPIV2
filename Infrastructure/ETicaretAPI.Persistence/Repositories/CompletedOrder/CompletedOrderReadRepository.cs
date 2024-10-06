using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class CompletedOrderReadRepository(ETicaretAPIDbContext context) : ReadRepository<CompletedOrder>(context), ICompletedOrderReadRepository
    {
    }
}
