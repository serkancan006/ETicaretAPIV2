namespace ETicaretAPI.Application.Features.Commands.Payment.CreatePayment
{
    public class CreatePaymentCommandResponse
    {
        public string? PaymentId { get; set; }
        public string? ConversationId { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
