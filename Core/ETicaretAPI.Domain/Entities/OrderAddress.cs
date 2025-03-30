using ETicaretAPI.Domain.Entities.Common;

namespace ETicaretAPI.Domain.Entities
{
    public class OrderAddress : BaseEntity
    {
        public string OrderAdresTitle { get; set; } // ev, iş yeri, diğer
        public string OrderAdres { get; set; } // açık adresi
        public string OrderCity { get; set; }  // şehir
        public string OrderNeighbourHood { get; set; } // mahalle
        public string OrderStreet { get; set; } // cadde , sokak
        public string OrderBuildingNumber { get; set; } // kapı no
        public string OrderApartmentNumber { get; set; } // apartman no
        public string OrderFloor { get; set; } // kat no


        public Order OrderShipping { get; set; }
        public Order OrderBilling { get; set; }
    }
}
