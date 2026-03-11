using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderService.Application.DbModels
{
    [Table("Orders")]
    public class DbOrder
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }
        
        public Guid ProductId { get; set; }
        
        [Required]
        public int Count { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        
        [Required]
        public int Status { get; set; }
    }
}