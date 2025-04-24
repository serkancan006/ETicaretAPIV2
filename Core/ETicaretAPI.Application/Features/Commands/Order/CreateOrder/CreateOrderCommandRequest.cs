using ETicaretAPI.Domain.Enums;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        public string Description { get; set; }
        public string ShippingId { get; set; } //Nakliye
        public string? BillingId { get; set; } //Fatura
        public bool UseBillingAsShipping { get; set; } = true; // Fatura adresi nakliye adresiyle aynı olsun mu 1 ise olsun 0 ise olmasın fatura adresi gir


        public PaymentTypeEnum PaymentType { get; set; } // Kapıda ödeme / online ödeme -> online ise aşağıdaki bilgileri al
        public string? CardHolderName { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpireMonth { get; set; }
        public string? ExpireYear { get; set; }
        public string? CVC { get; set; }
    }
}
