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


        public string OrderCode { get; set; } 
        public Basket Basket { get; set; }


        // Adresler
        //Shipping
        public string ShippingOrderAdresTitle { get; set; } // ev, iş yeri, diğer
        public string ShippingOrderAdres { get; set; } // açık adresi
        public string ShippingOrderCity { get; set; }  // şehir
        public string ShippingOrderNeighbourHood { get; set; } // mahalle
        public string ShippingOrderStreet { get; set; } // cadde , sokak
        public string ShippingOrderBuildingNumber { get; set; } // kapı no
        public string ShippingOrderApartmentNumber { get; set; } // apartman no
        public string ShippingOrderFloor { get; set; } // kat no
        // Billing
        public string BillingOrderAdresTitle { get; set; } // ev, iş yeri, diğer
        public string BillingOrderAdres { get; set; } // açık adresi
        public string BillingOrderCity { get; set; }  // şehir
        public string BillingOrderNeighbourHood { get; set; } // mahalle
        public string BillingOrderStreet { get; set; } // cadde , sokak
        public string BillingOrderBuildingNumber { get; set; } // kapı no
        public string BillingOrderApartmentNumber { get; set; } // apartman no
        public string BillingOrderFloor { get; set; } // kat no


    }
}
