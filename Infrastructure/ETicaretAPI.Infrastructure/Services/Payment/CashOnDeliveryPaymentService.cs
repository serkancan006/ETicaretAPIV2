using ETicaretAPI.Application.Abstractions.Payment;
using ETicaretAPI.Application.DTOs.Payment;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Infrastructure.Services.Payments
{
    public class CashOnDeliveryPaymentService : IPaymentService
    {
        public async Task<PaymentResult> ProcessPaymentAsync(Order order, CreatePaymentCard createPaymentCard)
        {
            Console.WriteLine("📦 Kapıda ödeme işleniyor...");

            var total = order.Basket.BasketItems.Sum(x => x.Product.Price * x.Quantity);
            if (total <= 0)
            {
                return await Task.FromResult(new PaymentResult
                {
                    IsSuccess = false,
                    Message = "İşlem Başarısız",
                    ErrorMessage = "Ödeme tutarı sıfır veya negatif olamaz"
                });
            }

            return await Task.FromResult(new PaymentResult
            {
                IsSuccess = true,
                Message = "Sipariş Oluşturuldu",
            });

        }

    }
}
