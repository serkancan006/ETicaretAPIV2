using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;


namespace ETicaretAPI.Persistence.Repositories.Category
{
    public class SubCategoryWriteRepository : WriteRepository<SubCategory>, ISubCategoryWriteRepository
    {
        public SubCategoryWriteRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
