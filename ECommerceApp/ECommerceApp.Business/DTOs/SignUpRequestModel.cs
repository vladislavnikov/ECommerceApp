using System.ComponentModel.DataAnnotations;
namespace ECommerceApp.Business.DTOs
{
    public class SignUpRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[^A-Za-z\d]).{6,}$", ErrorMessage = "Password must contain at least one letter and one non-alphabetic character.")]
        public string Password { get; set; }
    }
}
