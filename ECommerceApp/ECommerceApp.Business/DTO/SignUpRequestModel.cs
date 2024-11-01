using System.ComponentModel.DataAnnotations;
namespace ECommerceApp.Business.DTOs
{
    /// <summary>
    /// Model for user sign-up requests.
    /// </summary>
    public class SignUpRequestModel
    {
        /// <summary>
        /// The user's email address.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The user's password.
        /// </summary>
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[^A-Za-z\d]).{6,}$", ErrorMessage = "Password must contain at least one letter and one non-alphabetic character.")]
        public string Password { get; set; }
    }
}
