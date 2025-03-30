using ETicaretAPI.Application.DTOs.Payment;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Abstractions.Payment
{
    public interface IOnlinePaymentGateway
    {
        Task<PaymentResult> CreatePaymentAsync(Order order, CreatePaymentCard createPaymentCard);
    }
}
