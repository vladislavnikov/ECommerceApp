namespace ECommerceApp.Model.Request
{
    public class OrderUpdateRequest
    {
        public int OrderId { get; set; }
        public List<OrderItemUpdateRequest> Updates { get; set; } = new();
    }
}
