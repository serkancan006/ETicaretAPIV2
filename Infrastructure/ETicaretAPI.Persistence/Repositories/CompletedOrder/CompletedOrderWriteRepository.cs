using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;

namespace ETicaretAPI.Persistence.Repositories
{
    public class CompletedOrderWriteRepository(ETicaretAPIDbContext context) : WriteRepository<CompletedOrder>(context), ICompletedOrderWriteRepository
    {

    }
}
