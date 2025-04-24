using ETicaretAPI.Application.Abstractions.Payments;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.DTOs.Payments;
using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Infrastructure.Services.Payments
{
    public class CashOnDeliveryPaymentService(IBasketReadRepository basketReadRepository) : IPaymentService
    {
        public async Task<PaymentResult> ProcessPaymentAsync(CreateOrder createOrder, CreatePaymentCard createPaymentCard)
        {
            var basket = await basketReadRepository.Table.Include(x => x.User).Include(x => x.BasketItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == Guid.Parse(createOrder.BasketId));
            if (createOrder == null)
            {
                return await Task.FromResult(new PaymentResult
                {
                    IsSuccess = false,
                    Message = "İşlem Başarısız",
                    ErrorMessage = "Sipariş bulunamadı"
                });
            }
            Console.WriteLine("📦 Kapıda ödeme işleniyor...");

            var total = basket.BasketItems.Sum(x => x.Product.Price * x.Quantity);
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
