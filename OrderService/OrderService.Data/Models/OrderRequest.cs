using System.ComponentModel.DataAnnotations;

namespace OrderService.Application.Models
{
    public record OrderRequest
    {
        public string ProductName { get; set; }
        
        public int Count { get; set; }
        
        [MaxLength(100)]
        public string Email { get; set; }
    }
}