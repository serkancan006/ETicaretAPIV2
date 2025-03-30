using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string? BasketId { get; set; }
        public string Description { get; set; }


        public PaymentTypeEnum PaymentType { get; set; } // Kapıda ödeme / online ödeme

        public Guid OrderAddressShippingId { get; set; }
        public Guid OrderAddressBillingId { get; set; }
    }
}
