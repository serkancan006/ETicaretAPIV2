using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Abstractions.Payments;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Payments;
using ETicaretAPI.Application.DTOs.UserAddress;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Order.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
    {
        readonly IOrderService _orderService;
        readonly IBasketService _basketService;
        readonly IOrderHubService _orderHubService;
        readonly IEnumerable<IPaymentService> _paymentServices;
        readonly IPaymentHelpers _helpersInfrastructure;
        readonly IUserAddressService _userAddressService;

        public CreateOrderCommandHandler(IOrderService orderService, IBasketService basketService, IOrderHubService orderHubService, IEnumerable<IPaymentService> paymentServices, IPaymentHelpers helpersInfrastructure, IUserAddressService userAddressService)
        {
            _orderService = orderService;
            _basketService = basketService;
            _orderHubService = orderHubService;
            _paymentServices = paymentServices;
            _helpersInfrastructure = helpersInfrastructure;
            _userAddressService = userAddressService;
        }

        public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
            SingleUserAddress shippedAddress = await _userAddressService.GetByUserAddressAsync(request.ShippingId);
            SingleUserAddress billingAddress;
            if (request.UseBillingAsShipping)
                billingAddress = shippedAddress;
            else
                try
                {
                    billingAddress = await _userAddressService.GetByUserAddressAsync(request.BillingId);
                }
                catch (Exception e)
                {
                    billingAddress = shippedAddress;
                }

            var order = new DTOs.Order.CreateOrder()
            {
                Description = request.Description,
                BasketId = _basketService.GetUserActiveBasket?.Id.ToString(),
                BillingOrderAdres = billingAddress.UserAdres,
                BillingOrderAdresTitle = billingAddress.UserAdresTitle,
                BillingOrderApartmentNumber = billingAddress.UserApartmentNumber,
                BillingOrderBuildingNumber = billingAddress.UserBuildingNumber,
                BillingOrderCity = billingAddress.UserCity,
                BillingOrderFloor = billingAddress.UserFloor,
                BillingOrderNeighbourHood = billingAddress.UserNeighbourHood,
                BillingOrderStreet = billingAddress.UserStreet,
                PaymentType = request.PaymentType,
                ShippingOrderAdres = shippedAddress.UserAdres,
                ShippingOrderAdresTitle = shippedAddress.UserAdresTitle,
                ShippingOrderApartmentNumber = shippedAddress.UserApartmentNumber,
                ShippingOrderBuildingNumber = shippedAddress.UserBuildingNumber,
                ShippingOrderCity = shippedAddress.UserCity,
                ShippingOrderFloor = shippedAddress.UserFloor,
                ShippingOrderNeighbourHood = shippedAddress.UserNeighbourHood,
                ShippingOrderStreet = shippedAddress.UserStreet,
            };



            IPaymentService? paymentService = _paymentServices
               .FirstOrDefault(service => _helpersInfrastructure.IsPaymentMethodMatch(service, order.PaymentType));

            if (paymentService == null)
                throw new Exception("Ödeme yöntemi bulunamadı!");

            PaymentResult result = await paymentService.ProcessPaymentAsync(order, new CreatePaymentCard()
            {
                CardHolderName = request.CardHolderName,
                CardNumber = request.CardNumber,
                CVC = request.CVC,
                ExpireMonth = request.ExpireMonth,
                ExpireYear = request.ExpireYear
            });

            if (result.IsSuccess == false)
                throw new Exception(result.ErrorMessage);
            else
            {
                order.PaymentId = result.PaymentId;
                order.ConversationId = result.ConversationId;

                await _orderService.CreateOrderAsync(order);
                await _orderHubService.OrderAddedMessageAsync("Heyy, yeni bir sipariş geldi! :) ");
            }

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
