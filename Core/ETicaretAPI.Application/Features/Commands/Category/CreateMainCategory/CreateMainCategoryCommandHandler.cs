using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Category.CreateMainCategory
{
    public class CreateMainCategoryCommandHandler(ICategoryService categoryService) : IRequestHandler<CreateMainCategoryCommandRequest, CreateMainCategoryCommandResponse>
    {
        public async Task<CreateMainCategoryCommandResponse> Handle(CreateMainCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            await categoryService.CreateMainCategoryAsync(new()
            { 
                ImagePath = request.ImagePath,
                Name = request.Name
            });
            return new();
        }
    }
}
