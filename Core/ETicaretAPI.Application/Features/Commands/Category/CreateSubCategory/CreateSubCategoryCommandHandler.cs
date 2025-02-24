using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Category.CreateSubCategory
{
    public class CreateSubCategoryCommandHandler(ICategoryService categoryService) : IRequestHandler<CreateSubCategoryCommandRequest, CreateSubCategoryCommandResponse>
    {
        public async Task<CreateSubCategoryCommandResponse> Handle(CreateSubCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await categoryService.CreateSubCategoryAsync(new()
            {
                ImagePath = request.ImagePath,
                Name = request.Name,
                MainCategoryId = Guid.Parse(request.MainCategoryId)
            });
            return new();
        }
    }
}
