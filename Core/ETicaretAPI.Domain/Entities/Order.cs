using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }


        public OrderStatusEnum OrderStatus { get; set; } // hazırlanıyor , beklemede
        public PaymentTypeEnum? PaymentType { get; set; } // Kapıda ödeme / online ödeme


        public string OrderCode { get; set; } = Guid.NewGuid().ToString();
        public Basket Basket { get; set; }
        public Guid? OrderAddressShippingId { get; set; }
        public OrderAddress? OrderAddressShipping { get; set; }
        public Guid? OrderAddressBillingId { get; set; }
        public OrderAddress? OrderAddressBilling { get; set; }
        public Guid? OrderPaymentId { get; set; }
        public OrderPayment? OrderPayment { get; set; }
    }
}
