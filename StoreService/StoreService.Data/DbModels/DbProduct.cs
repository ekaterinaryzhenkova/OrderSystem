using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreService.Data.DbModels
{
    [Table("Products")]
    public class DbProduct
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Quantity { get; set; }
    }
}