using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Repositories.Category
{
    public class MainCategoryReadRepository : ReadRepository<MainCategory>, IMainCategoryReadRepository
    {
        private ETicaretAPIDbContext _context;
        public MainCategoryReadRepository(ETicaretAPIDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<MainCategory>> GetAllWithSubCategoryAsync()
        {
            return await _context.MainCategories.Include(x => x.SubCategories).ToListAsync();
        }
    }
}
