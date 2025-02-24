
namespace ETicaretAPI.Application.DTOs.Category
{
    public class ListSubCategory
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }
        public Guid MainCategoryId { get; set; }
    }
}
