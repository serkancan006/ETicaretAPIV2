
namespace ETicaretAPI.Application.DTOs.Category
{
    public class UpdateSubCategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Guid MainCategoryId { get; set; }
    }
}
