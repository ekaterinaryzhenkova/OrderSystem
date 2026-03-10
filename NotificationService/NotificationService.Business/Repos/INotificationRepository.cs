using NotificationService.Application.DbModels;

namespace NotificationService.Business.Repos
{
    public interface INotificationRepository
    {
        Task<DbNotification> CreateAsync(DbNotification notification);
    }
}