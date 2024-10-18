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
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Password must contain letters and numbers.")]
        public string Password { get; set; }
    }
}
