using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Payment.CreatePayment
{
    public class CreatePaymentCommandRequest : IRequest<CreatePaymentCommandResponse>
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CVC { get; set; }
        public string OrderId { get; set; }
    }
}
