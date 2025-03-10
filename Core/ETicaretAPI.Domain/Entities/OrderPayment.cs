using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Domain.Enums;

namespace ETicaretAPI.Domain.Entities
{
    public class OrderPayment : BaseEntity
    {
        public string CardToken { get; set; }  // Card Token 
        public string TransactionToken { get; set; }  // İşlem Token
        public string TransactionId { get; set; }  // İşlem ID si
        public decimal Amount { get; set; }  // İşlem Tutarı
        public PaymentStatusEnum PaymentStatus { get; set; }  // Payment status: Pending, Paid, Failed, Refunded


        //// Taksitli ödeme için gerekli özellikler
        //public int? InstallmentCount { get; set; }  // Taksit Sayısı
        //public List<decimal> InstallmentAmounts { get; set; }  // Taksit Tutarları
        //public decimal? InitialPaymentAmount { get; set; }  // İlk ödeme tutarı
        //public List<PaymentStatusEnum> InstallmentPaymentStatus { get; set; }  // Her bir taksitin ödeme durumu

        public Order? Order { get; set; }
    }
}
