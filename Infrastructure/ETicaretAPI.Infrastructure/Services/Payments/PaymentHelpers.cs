using ETicaretAPI.Application.Abstractions.Payments;
using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Infrastructure.Services.Payments
{
    public class PaymentHelpers : IPaymentHelpers
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
