namespace ETicaretAPI.Application.DTOs.Payments
{
    public class PaymentResult
    {
        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }
        public string? PaidPrice { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
