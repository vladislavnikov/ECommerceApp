namespace ECommerceApp.Business.DTO.Order
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
