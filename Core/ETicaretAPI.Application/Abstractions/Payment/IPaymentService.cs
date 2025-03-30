using ETicaretAPI.Application.DTOs.Payment;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Abstractions.Payment
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(Order order, CreatePaymentCard createPaymentCard);
    }
}
