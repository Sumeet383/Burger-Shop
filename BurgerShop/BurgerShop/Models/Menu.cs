using System.ComponentModel.DataAnnotations;

namespace BurgerShop.Models
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }
        public string? BurgerName { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public int BasePrice { get; set; }
    }
}
