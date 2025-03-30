using ETicaretAPI.Domain.Entities.Common;


namespace ETicaretAPI.Domain.Entities
{
    public class MainCategory : BaseEntity
    {
        public string Name { get; set; }
        public string? ImagePath { get; set; }


        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
