using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Business.Model.Request
{
    /// <summary>
    /// Model for updating user profile information.
    /// </summary>
    public class UserUpdateRequest
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The email of the user.
        /// </summary>
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format. Please use an international format starting with '+'.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The address of the user.
        /// </summary>
        public string AddressDelivery { get; set; }
    }
}
