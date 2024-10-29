using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Business.Model.Request
{
    public class UserUpdateRequest
    {
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format. Please use an international format starting with '+'.")]
        public string PhoneNumber { get; set; }
        public string AddressDelivery { get; set; }
    }
}
