using ETicaretAPI.Application.Abstractions.Payments;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.DTOs.Payments;
using ETicaretAPI.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Infrastructure.Services.Payments
{
    class OnlinePaymentService(IOnlinePaymentGateway paymentGateway, IBasketReadRepository basketReadRepository) : IPaymentService
    {
        public async Task<PaymentResult> ProcessPaymentAsync(CreateOrder createOrder, CreatePaymentCard createPaymentCard)
        {
            var basket = await basketReadRepository.Table.Include(x => x.User).Include(x => x.BasketItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == Guid.Parse(createOrder.BasketId));
            if (createOrder == null)
                throw new Exception("Order not found");

            var result = await paymentGateway.CreatePaymentAsync(createOrder, createPaymentCard, basket);

            if (result.IsSuccess == false)
            {
                createOrder.ConversationId = result.ConversationId;
            }
            else
            {
                createOrder.ConversationId = result.ConversationId;
            }

            return result;
        }
       
    }
}
