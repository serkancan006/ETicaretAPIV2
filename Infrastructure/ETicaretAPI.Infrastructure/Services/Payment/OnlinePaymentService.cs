using ETicaretAPI.Application.Abstractions.Payment;
using ETicaretAPI.Application.DTOs.Payment;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Infrastructure.Services.Payments
{
    class OnlinePaymentService(IOnlinePaymentGateway paymentGateway) : IPaymentService
    {
        public async Task<PaymentResult> ProcessPaymentAsync(Order order, CreatePaymentCard createPaymentCard)
        {
            var result = await paymentGateway.CreatePaymentAsync(order, createPaymentCard);

            if (result.IsSuccess == false)
            {
                order.ConversationId = result.ConversationId;
            }
            else
            {
                order.OrderStatus = Domain.Enums.OrderStatusEnum.Preparing;
                order.ConversationId = result.ConversationId;
            }

            return result;
        }
       
    }
}
