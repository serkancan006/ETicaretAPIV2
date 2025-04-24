using ETicaretAPI.Application.Abstractions.Payments;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.DTOs.Payments;
using ETicaretAPI.Application.Repositories;

namespace ETicaretAPI.Infrastructure.Services.Payments
{
    class OnlinePaymentService(IOnlinePaymentGateway paymentGateway, IOrderReadRepository orderReadRepository) : IPaymentService
    {
        public async Task<PaymentResult> ProcessPaymentAsync(CreateOrder createOrder, CreatePaymentCard createPaymentCard)
        {
            var order = await orderReadRepository.GetByIdAsync(createOrder.BasketId);
            if (order == null)
                throw new Exception("Order not found");

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
