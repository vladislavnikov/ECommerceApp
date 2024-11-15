namespace ECommerceApp.Business.DTO.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsPaid { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
    }
}
