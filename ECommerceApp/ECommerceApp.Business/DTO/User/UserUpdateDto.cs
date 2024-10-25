using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Business.DTO.User
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [RegularExpression(@"^\+?[0-9]\d{1,14}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }
        public string AddressDelivery { get; set; }
    }
}
