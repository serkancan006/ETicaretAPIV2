using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.DTOs.Payments;

namespace ETicaretAPI.Application.Abstractions.Payments
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(CreateOrder order, CreatePaymentCard createPaymentCard);
    }
}
