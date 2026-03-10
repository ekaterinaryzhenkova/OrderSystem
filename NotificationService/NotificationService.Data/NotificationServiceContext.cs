using Microsoft.EntityFrameworkCore;
using NotificationService.Application.DbModels;

namespace NotificationService.Application
{
    public class NotificationServiceContext(
        DbContextOptions<NotificationServiceContext> options)
        : DbContext(options)
    {
        public DbSet<DbNotification> Notifications { get; set; }
    }
}