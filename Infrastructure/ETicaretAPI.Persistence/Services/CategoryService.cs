using AutoMapper;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Category;
using ETicaretAPI.Application.Repositories.Category;
using ETicaretAPI.Domain.Entities;


namespace ETicaretAPI.Persistence.Services
{
    public class CategoryService(IMainCategoryReadRepository mainCategoryReadRepository, IMainCategoryWriteRepository mainCategoryWriteRepository, ISubCategoryWriteRepository subCategoryWriteRepository, ISubCategoryReadRepository subCategoryReadRepository,IMapper mapper) : ICategoryService
    {
        public async Task CreateMainCategoryAsync(CreateMainCategory createMainCategory)
        {
            var value = mapper.Map<MainCategory>(createMainCategory);
            await mainCategoryWriteRepository.AddAsync(value);
            await mainCategoryWriteRepository.SaveAsync();
        }

        public async Task CreateSubCategoryAsync(CreateSubCategory createSubCategory)
        {
            var value = mapper.Map<SubCategory>(createSubCategory);
            await subCategoryWriteRepository.AddAsync(value);
            await subCategoryWriteRepository.SaveAsync();
        }

        public async Task<List<ListCategory>> GetCategoriesAsync()
        {
            var values = await mainCategoryReadRepository.GetAllWithSubCategoryAsync();
            return mapper.Map<List<ListCategory>>(values);
        }

        public async Task RemoveMainCategoryAsync(string mainCategoryId)
        {
            await mainCategoryWriteRepository.RemoveAsync(mainCategoryId);
            await mainCategoryWriteRepository.SaveAsync();
        }

        public async Task RemoveSubCategoryAsync(string subCategoryId)
        {
            await subCategoryWriteRepository.RemoveAsync(subCategoryId);
            await subCategoryWriteRepository.SaveAsync();
        }

        public async Task UpdateMainCategoryAsync(UpdateMainCategory updateMainCategory)
        {
            var value = mapper.Map<MainCategory>(updateMainCategory);
            mainCategoryWriteRepository.Update(value);
            await mainCategoryWriteRepository.SaveAsync();
        }

        public async Task UpdateSubCategoryAsync(UpdateSubCategory updateSubCategory)
        {
            var value = mapper.Map<SubCategory>(updateSubCategory);
            subCategoryWriteRepository.Update(value);
            await subCategoryWriteRepository.SaveAsync();
        }
    }
}
