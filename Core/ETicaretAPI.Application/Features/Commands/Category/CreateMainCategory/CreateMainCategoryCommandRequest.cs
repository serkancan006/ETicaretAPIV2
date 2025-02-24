using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Category.CreateMainCategory
{
    public class CreateMainCategoryCommandRequest : IRequest<CreateMainCategoryCommandResponse>
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
    }
}
