using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Business.Model.Request
{
    public class PasswordUpdateRequest
    {
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[^A-Za-z\d]).{6,}$", ErrorMessage = "Password must contain at least one letter and one non-alphabetic character.")]
        public string OldPassword { get; set; }

        [MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[^A-Za-z\d]).{6,}$", ErrorMessage = "Password must contain at least one letter and one non-alphabetic character.")]
        public string NewPassword { get; set; }
    }
}
