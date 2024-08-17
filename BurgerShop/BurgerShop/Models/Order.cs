using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BurgerShop.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        public string? BurgerType { get; set; }
        [Required]
        public string? BurgerCategory { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
    }
}
