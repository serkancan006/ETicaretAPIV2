using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace ETicaretAPI.Infrastructure.Services.Payments.Iyzico
{
    public class IyzicoClient
    {
        private readonly Options _options;

        public IyzicoClient(IConfiguration configuration)
        {
            _options = new Options
            {
                ApiKey = configuration["Payment:Iyzico:ApiKey"],
                SecretKey = configuration["Payment:Iyzico:SecretKey"],
                BaseUrl = configuration["Payment:Iyzico:BaseUrl"]
            };
        }

        /// <summary>
        /// Yeni bir ödeme oluşturur.
        /// </summary>
        public async Task<Payment> CreatePaymentAsync(CreatePaymentRequest paymentRequest)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Payment.Create(paymentRequest, _options);
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
        public async Task<Payment> RetrievePaymentAsync(string paymentId)
        {
            return await Task.Run(() =>
            {
                try
                {
                    RetrievePaymentRequest request = new RetrievePaymentRequest { PaymentId = paymentId };
                    return Payment.Retrieve(request, _options);
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
        public async Task<Card> CreateUserCardAsync(CreateCardRequest cardRequest)
        {
            return await Task.Run(() =>
            {
                try
                {
                    return Card.Create(cardRequest, _options);
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
        public async Task<bool> DeleteUserCardAsync(DeleteCardRequest deleteCardRequest)
        {
            return await Task.Run(() =>
            {
                try { Card.Delete(deleteCardRequest, _options); return true; }
                catch (Exception ex) { Console.WriteLine("Kart silme başarısız: " + ex.Message); return false; }
            });
        }

        /// <summary>
        /// Ödeme işlemini tekrar dener.
        /// </summary>
        public async Task<Payment> RetryPaymentAsync(CreatePaymentRequest paymentRequest)
        {
            return await CreatePaymentAsync(paymentRequest);
        }

        /// <summary>
        /// Belirtilen ödeme ID'sine göre ödemeyi iptal eder.
        /// </summary>
        public async Task<Cancel> CancelPaymentAsync(string paymentId, string ip)
        {
            return await Task.Run(() =>
            {
                try { return Cancel.Create(new CreateCancelRequest { PaymentId = paymentId, Ip = ip }, _options); }
                catch (Exception ex) { Console.WriteLine("Ödeme iptal edilemedi: " + ex.Message); return null; }
            });
        }

        /// <summary>
        /// Belirtilen ödeme işlem kimliğine göre iade işlemi gerçekleştirir.
        /// </summary>
        public async Task<Refund> RefundPaymentAsync(string paymentTransactionId, decimal amount, string ip)
        {
            return await Task.Run(() =>
            {
                try { return Refund.Create(new CreateRefundRequest { PaymentTransactionId = paymentTransactionId, Price = amount.ToString("F2", CultureInfo.InvariantCulture), Ip = ip }, _options); }
                catch (Exception ex) { Console.WriteLine("İade işlemi başarısız: " + ex.Message); return null; }
            });
        }

        /// <summary>
        /// Kullanıcının kayıtlı kartlarını getirir.
        /// </summary>
        public async Task<CardList> GetUserCards(string userId)
        {
            try
            {
                RetrieveCardListRequest request = new RetrieveCardListRequest { CardUserKey = userId };
                CardList cardList = await CardList.Retrieve(request, _options);
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
        public async Task<List<Payment>> GetUserPaymentsAsync(string userId)
        {
            try
            {
                RetrievePaymentRequest request = new() { PaymentId = userId };
                var payment = await Task.Run(() => Payment.Retrieve(request, _options));
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
        public async Task<bool> CheckUserPaymentLimitAsync(string userId, decimal maxLimit)
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
        public async Task<bool> DeleteAllUserCardsAsync(string userId)
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
