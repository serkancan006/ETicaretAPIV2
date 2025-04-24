using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Application.DTOs.Order
{
    public class CreateOrder
    {
        public string? BasketId { get; set; }   // Authentication Basketden  alınıyor 
        public string Description { get; set; }

        //public OrderStatusEnum OrderStatus { get; set; } // hazırlanıyor , beklemede servisten direk veriyoruz
        public PaymentTypeEnum PaymentType { get; set; } // Kapıda ödeme / online ödeme
        public string? PaymentId { get; set; }  // İşlem ID si
        public string? ConversationId { get; set; }  // İşlem ID si
        // public PaymentStatusEnum PaymentStatus { get; set; }  // Payment status: Pending, Paid, Failed, Refunded -> daha eklenmedi bu sütun
        //public string OrderCode { get; set; } servisten direk veriyoruz

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
