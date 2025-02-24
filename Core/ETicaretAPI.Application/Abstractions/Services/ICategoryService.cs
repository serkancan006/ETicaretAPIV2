using ETicaretAPI.Application.DTOs.Category;


namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<List<ListCategory>> GetCategoriesAsync();

        Task CreateMainCategoryAsync(CreateMainCategory createMainCategory);
        Task UpdateMainCategoryAsync(UpdateMainCategory updateMainCategory);
        Task RemoveMainCategoryAsync(string mainCategoryId);

        Task CreateSubCategoryAsync(CreateSubCategory createSubCategory);
        Task UpdateSubCategoryAsync(UpdateSubCategory updateSubCategory);
        Task RemoveSubCategoryAsync(string subCategoryId);
    }
}
