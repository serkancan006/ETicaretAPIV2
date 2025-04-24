using ETicaretAPI.Application.Abstractions.Payments;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.DTOs.Payments;
using ETicaretAPI.Application.Repositories;

namespace ETicaretAPI.Infrastructure.Services.Payments
{
    public class CashOnDeliveryPaymentService(IOrderReadRepository orderReadRepository) : IPaymentService
    {
        public async Task<PaymentResult> ProcessPaymentAsync(CreateOrder createOrder, CreatePaymentCard createPaymentCard)
        {
            var order = await orderReadRepository.GetByIdAsync(createOrder.BasketId);
            if (order == null)
            {
                return await Task.FromResult(new PaymentResult
                {
                    IsSuccess = false,
                    Message = "İşlem Başarısız",
                    ErrorMessage = "Sipariş bulunamadı"
                });
            }
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
