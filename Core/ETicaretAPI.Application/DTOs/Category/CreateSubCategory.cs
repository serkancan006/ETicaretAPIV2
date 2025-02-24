
namespace ETicaretAPI.Application.DTOs.Category
{
    public class CreateSubCategory
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Guid MainCategoryId { get; set; }
    }
}
