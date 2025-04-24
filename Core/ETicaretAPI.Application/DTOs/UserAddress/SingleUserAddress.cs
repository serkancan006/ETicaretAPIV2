namespace ETicaretAPI.Application.DTOs.UserAddress
{
    public class SingleUserAddress
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
    }
}
