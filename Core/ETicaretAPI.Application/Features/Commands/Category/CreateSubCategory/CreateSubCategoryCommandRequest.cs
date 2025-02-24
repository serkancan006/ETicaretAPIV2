using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Category.CreateSubCategory
{
    public class CreateSubCategoryCommandRequest : IRequest<CreateSubCategoryCommandResponse>
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string MainCategoryId { get; set; }
    }
}
