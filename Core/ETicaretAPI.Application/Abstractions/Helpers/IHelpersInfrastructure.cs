using ETicaretAPI.Application.Abstractions.Payment;
using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Application.Abstractions.Helpers
{
    public interface IHelpersInfrastructure
    {
        bool IsPaymentMethodMatch(IPaymentService service, PaymentTypeEnum paymentType);
    }
}
