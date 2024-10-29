namespace ECommerceApp.Business.Model.Response
{
    public class UserInfoResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressDelivery { get; set; }
        public DateTime AccountCreationDate { get; set; }
    }
}
