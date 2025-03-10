namespace ETicaretAPI.Domain.Enums
{
    public enum OrderStatusEnum
    {
        Pending = 1,         // Sipariş beklemede
        Preparing = 2,       // Sipariş hazırlanıyor
        Shipped = 3,         // Sipariş kargoya verildi
        Delivered = 4,       // Sipariş teslim edildi
        Canceled = 5,        // Sipariş iptal edildi
        Returned = 6,        // Sipariş iade edildi
        Completed = 7        // Sipariş tamamlandı
    }
}
