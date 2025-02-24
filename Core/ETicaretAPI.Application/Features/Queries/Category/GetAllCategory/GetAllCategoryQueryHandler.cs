using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Category.GetAllCategory
{
    public class GetAllCategoryQueryHandler(ICategoryService categoryService) : IRequestHandler<GetAllCategoryQueryRequest, List<GetAllCategoryQueryResponse>>
    {
        public async Task<List<GetAllCategoryQueryResponse>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await categoryService.GetCategoriesAsync();
            return categories.Select(category => new GetAllCategoryQueryResponse
            {
                Id = category.Id.ToString(),
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate,
                Name = category.Name,
                ImagePath = category.ImagePath,
                SubCategories = category.SubCategories
            }).ToList();
        }
    }
}
