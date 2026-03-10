using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotificationService.Application.DbModels
{
    [Table("Notifications")]
    public class DbNotification
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid OrderId { get; set; }
        
        [Required]
        public string Message { get; set; }
        
        [Required]
        public bool IsSent { get; set; }
        
        public string? Exception { get; set; }
    }
}