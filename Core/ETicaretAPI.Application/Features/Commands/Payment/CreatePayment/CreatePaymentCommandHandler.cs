using ETicaretAPI.Application.Abstractions.Helpers;
using ETicaretAPI.Application.Abstractions.Payment;
using ETicaretAPI.Application.Abstractions.Services;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Payment.CreatePayment
{
    public class CreatePaymentCommandHandler(IOrderService orderService, IEnumerable<IPaymentService> _paymentServices, IHelpersInfrastructure helpersInfrastructure) : IRequestHandler<CreatePaymentCommandRequest, CreatePaymentCommandResponse>
    {
        public async Task<CreatePaymentCommandResponse> Handle(CreatePaymentCommandRequest request, CancellationToken cancellationToken)
        {
            var order = await orderService.GetRealOrderByIdAsync(request.OrderId);

            var paymentService = _paymentServices
               .FirstOrDefault(service => helpersInfrastructure.IsPaymentMethodMatch(service, order.PaymentType));

            var result = await paymentService.ProcessPaymentAsync(order, new()
            {
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                CVC = request.CVC,
                ExpireMonth = request.ExpireMonth,
                ExpireYear = request.ExpireYear 
            });


            return new()
            {
                ConversationId = result.ConversationId,
                ErrorMessage = result.ErrorMessage,
                IsSuccess = result.IsSuccess,
                Message = result.Message,
                PaymentId = result.PaymentId
            };
        }

       

    }
}
