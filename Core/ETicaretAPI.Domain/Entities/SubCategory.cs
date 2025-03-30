using ETicaretAPI.Domain.Entities.Common;


namespace ETicaretAPI.Domain.Entities
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }
        public string? ImagePath { get; set; }

        public Guid MainCategoryId { get; set; }
        public MainCategory MainCategory { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
