using ETicaretAPI.Application.DTOs.Payments;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Abstractions.Payments
{
    public interface IOnlinePaymentGateway
    {
        Task<PaymentResult> CreatePaymentAsync(Order order, CreatePaymentCard createPaymentCard);
    }
}
