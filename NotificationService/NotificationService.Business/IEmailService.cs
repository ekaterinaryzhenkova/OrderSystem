namespace NotificationService.Business
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string body);
    }
}