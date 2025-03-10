namespace ETicaretAPI.Domain.Enums
{
    public enum PaymentStatusEnum
    {
        Pending = 1,         // Ödeme beklemede
        Paid = 2,            // Ödeme yapıldı
        Failed = 3,          // Ödeme başarısız
        Refunded = 4,        // Ödeme iade edildi
        Cancelled = 5,       // Ödeme iptal edildi
         // Kısmi ödeme iade edildi (taksit,ön ödeme...)
    }
}
