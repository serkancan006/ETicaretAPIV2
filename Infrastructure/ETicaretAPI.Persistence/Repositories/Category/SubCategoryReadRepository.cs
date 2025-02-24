using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;


namespace ETicaretAPI.Persistence.Repositories.Category
{
    public class SubCategoryReadRepository : ReadRepository<SubCategory>, ISubCategoryReadRepository
    {
        public SubCategoryReadRepository(ETicaretAPIDbContext context) : base(context)
        {
        }
    }
}
