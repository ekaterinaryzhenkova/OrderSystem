using NotificationService.Application;
using NotificationService.Application.DbModels;

namespace NotificationService.Business.Repos
{
    public class NotificationRepository(NotificationServiceContext context) : INotificationRepository
    {
        public async Task<DbNotification> CreateAsync(DbNotification notification)
        {
            context.Notifications.Add(notification);
            await context.SaveChangesAsync();

            return notification;
        }
    }
}