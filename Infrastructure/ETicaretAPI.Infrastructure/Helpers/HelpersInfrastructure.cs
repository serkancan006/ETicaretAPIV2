using ETicaretAPI.Application.Abstractions.Helpers;
using ETicaretAPI.Application.Abstractions.Payment;
using ETicaretAPI.Domain.Enums;
using ETicaretAPI.Infrastructure.Services.Payments;

namespace ETicaretAPI.Infrastructure.Helpers
{
    public class HelpersInfrastructure : IHelpersInfrastructure
    {
        public bool IsPaymentMethodMatch(IPaymentService service, PaymentTypeEnum paymentType)
        {
            return service.GetType().Name switch
            {
                nameof(CashOnDeliveryPaymentService) => paymentType == PaymentTypeEnum.CashOnDelivery,
                nameof(OnlinePaymentService) => paymentType == PaymentTypeEnum.OnlinePayment,
                _ => false
            };
        }
    }
}
