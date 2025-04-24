using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Application.Abstractions.Payments
{
    public interface IPaymentHelpers
    {
        bool IsPaymentMethodMatch(IPaymentService service, PaymentTypeEnum paymentType);
    }
}
