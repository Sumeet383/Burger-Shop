using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BurgerShop.Models
{

    public class User
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Mobile No: ")]
        [Required(ErrorMessage = "Mobile number is required.")]
        [MaxLength(10, ErrorMessage = "Mobile number cannot exceed 10 digits.")]
        [MinLength(10, ErrorMessage = "Mobile number must be exactly 10 digits.")]
        public string? MobileNo { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Role { get; set; }



    }
}
