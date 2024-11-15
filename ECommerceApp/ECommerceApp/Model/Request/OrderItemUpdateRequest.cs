namespace ECommerceApp.Model.Request
{
    public class OrderItemUpdateRequest
    {
        public int ProductId { get; set; }
        public int NewQuantity { get; set; }
    }
}
