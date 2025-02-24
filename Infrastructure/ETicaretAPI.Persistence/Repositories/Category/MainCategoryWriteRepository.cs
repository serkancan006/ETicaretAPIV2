using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;


namespace ETicaretAPI.Persistence.Repositories.Category
{
    public class MainCategoryWriteRepository : WriteRepository<MainCategory>, IMainCategoryWriteRepository
    {
        public MainCategoryWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
