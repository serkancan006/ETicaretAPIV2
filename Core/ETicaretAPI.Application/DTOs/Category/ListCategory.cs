
namespace ETicaretAPI.Application.DTOs.Category
{
    public class ListCategory
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public string Name { get; set; }
        public string ImagePath { get; set; }

        public List<ListSubCategory> SubCategories { get; set; }
    }
}
