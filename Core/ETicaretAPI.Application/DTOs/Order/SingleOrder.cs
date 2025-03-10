using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class SingleOrder
    {
        public object BasketItems { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public string OrderCode { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
    }
}
