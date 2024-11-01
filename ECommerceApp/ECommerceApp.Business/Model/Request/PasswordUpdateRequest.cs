using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Business.Model.Request
{
    public class PasswordUpdateRequest
    {
        /// <summary>
        /// The old password of the user.
        /// </summary>
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[^A-Za-z\d]).{6,}$", ErrorMessage = "Password must contain at least one letter and one non-alphabetic character.")]
        public string OldPassword { get; set; }

        /// <summary>
        /// The new password of the user.
        /// </summary>
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*[^A-Za-z\d]).{6,}$", ErrorMessage = "Password must contain at least one letter and one non-alphabetic character.")]
        public string NewPassword { get; set; }
    }
}
