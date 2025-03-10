using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public string UserAdresTitle { get; set; }
        public string UserAdres { get; set; }
        public string UserCity { get; set; }
        public string UserNeighbourHood { get; set; }
        public string UserStreet { get; set; }
        public string UserBuildingNumber { get; set; }
        public string UserApartmentNumber { get; set; }
        public string UserFloor { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // public string UserAddressType { get; set; }    // Shipping - Billing
        // public string UserCountry { get; set; }
        // public string UserPostalCode { get; set; }
    }
}
