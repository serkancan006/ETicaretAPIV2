using ETicaretAPI.Domain.Entities;


namespace ETicaretAPI.Application.Repositories.Category
{
    public interface IMainCategoryReadRepository : IReadRepository<MainCategory>
    {
        Task<List<MainCategory>> GetAllWithSubCategoryAsync();
    }
}
