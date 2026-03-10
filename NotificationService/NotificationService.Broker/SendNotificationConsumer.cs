using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.DbModels;
using NotificationService.Business;
using NotificationService.Business.Repos;
using SharedModels;

namespace NotificationService.Broker;

public class SendNotificationConsumer(
    INotificationRepository repository,
    IEmailService emailService,
    ILogger<SendNotificationConsumer> logger)
    : IConsumer<OrderNotification>
{
    public async Task Consume(ConsumeContext<OrderNotification> context)
    {
        var response = context.Message;
        var notification = new DbNotification
        {
            Id = Guid.NewGuid(), Message = response.Message, OrderId = response.OrderId,
        };

        try
        {
            await emailService.SendEmailAsync(response.Email, response.Message);

            notification.IsSent = true;
            await repository.CreateAsync(notification);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Email sending for order {OrderId} failed", response.OrderId);
            
            notification.IsSent = false;
            notification.Exception = ex.Message;
            await repository.CreateAsync(notification);
        }
        
    }
}