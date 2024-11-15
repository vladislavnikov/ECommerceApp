namespace ECommerceApp.Model.Request
{
    public class OrderItemDeleteRequest
    {
        public int OrderId { get; set; }

        public List<int> ItemIds { get; set; } = new();
    }
}
