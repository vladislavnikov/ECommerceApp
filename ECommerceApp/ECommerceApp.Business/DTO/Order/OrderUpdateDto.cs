namespace ECommerceApp.Business.DTO.Order
{
    public class OrderUpdateDto
    {
        public int OrderId { get; set; }
        public List<OrderItemUpdateDto> Updates { get; set; } = new();
    }
}
