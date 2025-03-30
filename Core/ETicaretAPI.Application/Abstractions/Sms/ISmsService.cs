namespace ETicaretAPI.Application.Abstractions.Sms
{
    public interface ISmsService
    {
        Task<string> SendSmsAsync(string phoneNumber, string message, string messageTitle);
    }
}
