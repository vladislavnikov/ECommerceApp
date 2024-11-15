namespace ECommerceApp.Model.Response
{
    public class OrderResponseModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }  
        public decimal TotalPrice { get; set; }
        public List<OrderItemResponseModel> Items { get; set; }
    }
}
