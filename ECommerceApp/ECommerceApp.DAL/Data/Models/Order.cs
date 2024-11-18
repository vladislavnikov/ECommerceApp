using ECommerceApp.DAL.Data.Models.Enum;

namespace ECommerceApp.DAL.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public bool IsPaid { get; set; } = false;
        public decimal TotalAmount => Items.Sum(item => item.Amount * item.ProductPrice);
        public Status Status { get; set; } = Status.Pending;
    }
}
