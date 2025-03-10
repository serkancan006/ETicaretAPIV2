using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Domain.Entities
{
    public class OrderAddress : BaseEntity
    {
        public string OrderAdresTitle { get; set; }
        public string OrderAdres { get; set; }
        public string OrderCity { get; set; }
        public string OrderNeighbourHood { get; set; }
        public string OrderStreet { get; set; }
        public string OrderBuildingNumber { get; set; }
        public string OrderApartmentNumber { get; set; }
        public string OrderFloor { get; set; }

       
        public Order? OrderShipping { get; set; }
        public Order? OrderBilling { get; set; }
    }
}
