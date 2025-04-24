using System.Globalization;
using ETicaretAPI.Application.Abstractions.Payments;
using ETicaretAPI.Application.Abstractions.Payments.Iyzico;
using ETicaretAPI.Application.DTOs.Payments;
using ETicaretAPI.Domain.Entities;
using Iyzipay.Model;
using Iyzipay.Request;

namespace ETicaretAPI.Infrastructure.Services.Payments.Iyzico
{
    public class IyzicoService(IyzicoClient iyzicoClient) : IOnlinePaymentGateway, IIyzicoService
    {

        public async Task<PaymentResult> CreatePaymentAsync(Order order, CreatePaymentCard createPaymentCard)
        {
            try
            {
                PaymentCard paymentCard = new PaymentCard()
                {
                    CardHolderName = createPaymentCard.CardHolderName,
                    CardNumber = createPaymentCard.CardNumber,
                    ExpireMonth = createPaymentCard.ExpireMonth,
                    ExpireYear = createPaymentCard.ExpireYear,
                    Cvc = createPaymentCard.CVC
                };

                Buyer buyer = new Buyer()
                {
                    Id = order.Basket.User.Id.ToString(),
                    Name = order.Basket.User.Name,
                    Surname = order.Basket.User.Surname,
                    Email = order.Basket.User.Email,
                    IdentityNumber = order.Basket.User.Id.ToString(),
                    RegistrationAddress = order?.BillingOrderNeighbourHood + "/" + order?.BillingOrderCity + " " + order?.BillingOrderAdres + " " + order?.BillingOrderApartmentNumber + "/" + order?.BillingOrderBuildingNumber,
                    City = order?.BillingOrderCity,
                    Country = "Turkey",
                };

                Address shippingAddress = new Address
                {
                    ContactName = order.Basket.User.Name + " " + order.Basket.User.Surname,
                    City = order?.ShippingOrderCity,
                    Country = "Turkey",
                    Description = order?.ShippingOrderNeighbourHood + "/" + order?.ShippingOrderCity + " " + order?.ShippingOrderAdres + " " + order?.ShippingOrderApartmentNumber + "/" + order?.ShippingOrderBuildingNumber
                };

                Address billingAddress = new Address
                {
                    ContactName = order.Basket.User.Name + " " + order.Basket.User.Surname,
                    City = order?.BillingOrderCity,
                    Country = "Turkey",
                    Description = order?.BillingOrderNeighbourHood + "/" + order?.BillingOrderCity + " " + order?.BillingOrderAdres + " " + order?.BillingOrderApartmentNumber + "/" + order?.BillingOrderBuildingNumber
                };

                List<Iyzipay.Model.BasketItem> basketItems = order.Basket.BasketItems
                    .Select(item => new Iyzipay.Model.BasketItem
                    {
                        Id = item.Id.ToString(),
                        Name = item.Product.Name ?? "Ürün Adı Yok",
                        Category1 = item.Product.SubCategory?.MainCategory?.Name ?? "Ana Kategori Yok",
                        Category2 = item.Product.SubCategory?.Name ?? "Alt Kategori Yok",
                        ItemType = BasketItemType.PHYSICAL.ToString(),
                        Price = item.Product.Price.ToString("F2", CultureInfo.InvariantCulture)
                    })
                    .ToList();

                var price = order.Basket.BasketItems.Sum(x => x.Product.Price);

                CreatePaymentRequest createPayment = new CreatePaymentRequest()
                {
                    Locale = Locale.TR.ToString(),
                    ConversationId = order.OrderCode,
                    Price = price.ToString("F2", CultureInfo.InvariantCulture),
                    PaidPrice = price.ToString("F2", CultureInfo.InvariantCulture),
                    Currency = "TRY",
                    BasketId = order.Basket.Id.ToString(),
                    PaymentCard = paymentCard,
                    Buyer = buyer,
                    ShippingAddress = shippingAddress,
                    BillingAddress = billingAddress,
                    BasketItems = basketItems,
                };

                var result = await CreatePaymentAsync(createPayment);

                if (result.Status == "success")
                {
                    return new()
                    {
                        IsSuccess = true,
                        Message = "Ödeme Başarılı",
                        ConversationId = result?.ConversationId,
                        PaymentId = result?.PaymentId,
                        PaidPrice = result?.PaidPrice,
                    };
                }
                else
                {
                    return new()
                    {
                        IsSuccess = false,
                        Message = "Ödeme Başarısız",
                        ErrorMessage = result?.ErrorMessage
                    };
                }
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "Beklenmeyen bir hata oluştu.",
                    ErrorMessage = ex.Message
                };
            }
        }



        /// <summary>
        /// Yeni bir ödeme oluşturur.
        /// </summary>
        private async Task<Payment> CreatePaymentAsync(CreatePaymentRequest paymentRequest)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Payment.Create(paymentRequest, iyzicoClient.GetOptions());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ödeme Başarısız: " + ex.Message);
                    return null;
                }
            });
        }

        /// <summary>
        /// Belirtilen ödeme ID'sine göre ödeme detaylarını getirir.
        /// </summary>
        private async Task<Payment> RetrievePaymentAsync(string paymentId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    RetrievePaymentRequest request = new RetrievePaymentRequest { PaymentId = paymentId };
                    return Payment.Retrieve(request, iyzicoClient.GetOptions());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ödeme sorgulama hatası: " + ex.Message);
                    return null;
                }
            });
        }

        /// <summary>
        /// Kullanıcı için yeni bir kart oluşturur.
        /// </summary>
        private async Task<Card> CreateUserCardAsync(CreateCardRequest cardRequest)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Card.Create(cardRequest, iyzicoClient.GetOptions());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Kart kaydetme başarısız: " + ex.Message);
                    return null;
                }
            });
        }

        /// <summary>
        /// Kullanıcının belirli bir kartını siler.
        /// </summary>
        private async Task<bool> DeleteUserCardAsync(DeleteCardRequest deleteCardRequest)
        {
            return await Task.Run(() =>
            {
                try { Card.Delete(deleteCardRequest, iyzicoClient.GetOptions()); return true; }
                catch (Exception ex) { Console.WriteLine("Kart silme başarısız: " + ex.Message); return false; }
            });
        }

        /// <summary>
        /// Ödeme işlemini tekrar dener.
        /// </summary>
        private async Task<Payment> RetryPaymentAsync(CreatePaymentRequest paymentRequest)
        {
            return await CreatePaymentAsync(paymentRequest);
        }

        /// <summary>
        /// Belirtilen ödeme ID'sine göre ödemeyi iptal eder.
        /// </summary>
        private async Task<Cancel> CancelPaymentAsync(string paymentId, string ip)
        {
            return await Task.Run(() =>
            {
                try { return Cancel.Create(new CreateCancelRequest { PaymentId = paymentId, Ip = ip }, iyzicoClient.GetOptions()); }
                catch (Exception ex) { Console.WriteLine("Ödeme iptal edilemedi: " + ex.Message); return null; }
            });
        }

        /// <summary>
        /// Belirtilen ödeme işlem kimliğine göre iade işlemi gerçekleştirir.
        /// </summary>
        private async Task<Refund> RefundPaymentAsync(string paymentTransactionId, decimal amount, string ip)
        {
            return await Task.Run(() =>
            {
                try { return Refund.Create(new CreateRefundRequest { PaymentTransactionId = paymentTransactionId, Price = amount.ToString("F2", CultureInfo.InvariantCulture), Ip = ip }, iyzicoClient.GetOptions()); }
                catch (Exception ex) { Console.WriteLine("İade işlemi başarısız: " + ex.Message); return null; }
            });
        }

        /// <summary>
        /// Kullanıcının kayıtlı kartlarını getirir.
        /// </summary>
        private async Task<CardList> GetUserCards(string userId)
        {
            try
            {
                RetrieveCardListRequest request = new RetrieveCardListRequest { CardUserKey = userId };
                CardList cardList = await CardList.Retrieve(request, iyzicoClient.GetOptions());
                return cardList;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kullanıcı kartları alınamadı: " + ex.Message);
                return new CardList();
            }
        }

        /// <summary>
        /// Kullanıcının ödeme geçmişini getirir.
        /// </summary>
        private async Task<List<Payment>> GetUserPaymentsAsync(string userId)
        {
            try
            {
                RetrievePaymentRequest request = new() { PaymentId = userId };
                var payment = await Task.Run(() => Payment.Retrieve(request, iyzicoClient.GetOptions()));
                return payment != null ? new List<Payment> { payment } : new List<Payment>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ödeme geçmişi alınamadı: {ex.Message}");
                return new List<Payment>();
            }
        }

        /// <summary>
        /// Kullanıcının ödeme limiti dahilinde olup olmadığını kontrol eder.
        /// </summary>
        private async Task<bool> CheckUserPaymentLimitAsync(string userId, decimal maxLimit)
        {
            try
            {
                var payments = await GetUserPaymentsAsync(userId);
                decimal totalPaid = payments.Sum(p => Convert.ToDecimal(p.Price, CultureInfo.InvariantCulture));
                return totalPaid <= maxLimit;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcının ödeme limiti kontrol edilemedi: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Kullanıcının tüm kartlarını siler.
        /// </summary>
        private async Task<bool> DeleteAllUserCardsAsync(string userId)
        {
            try
            {
                var cardList = await GetUserCards(userId);
                bool isSuccess = true;
                foreach (var card in cardList.CardDetails)
                {
                    var deleteRequest = new DeleteCardRequest { CardUserKey = userId, CardToken = card.CardToken };
                    isSuccess &= await DeleteUserCardAsync(deleteRequest);
                }
                return isSuccess;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Kullanıcının kartları silinemedi: {ex.Message}");
                return false;
            }
        }
    }
}
