using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }

        // durum
        public OrderStatusEnum OrderStatus { get; set; } // hazırlanıyor , beklemede
        
        //ödeme
        public PaymentTypeEnum PaymentType { get; set; } // Kapıda ödeme / online ödeme
        
        //ödeme
        public string? PaymentId { get; set; }  // İşlem ID si
        public string? ConversationId { get; set; }  // İşlem ID si
        // public PaymentStatusEnum PaymentStatus { get; set; }  // Payment status: Pending, Paid, Failed, Refunded


        public string OrderCode { get; set; } = Guid.NewGuid().ToString();
        public Basket Basket { get; set; }
        public Guid OrderAddressShippingId { get; set; }
        public OrderAddress OrderAddressShipping { get; set; }
        public Guid OrderAddressBillingId { get; set; }
        public OrderAddress OrderAddressBilling { get; set; }
    }
}
