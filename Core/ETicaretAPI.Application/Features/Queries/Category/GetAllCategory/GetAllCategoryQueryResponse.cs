using ETicaretAPI.Application.DTOs.Category;

namespace ETicaretAPI.Application.Features.Queries.Category.GetAllCategory
{
    public class GetAllCategoryQueryResponse
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        public object SubCategories { get; set; }
    }
}
