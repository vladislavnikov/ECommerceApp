using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.Order;
using ECommerceApp.DAL.Data.Models;
using ECommerceApp.DAL.Data.Models.Enum;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDto> AddProductToOrderAsync(Guid userId, int productId, int amount)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ArgumentException("Invalid product ID");

            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Status.ToString() == "Pending");

            if (order == null)
            {
                order = new Order
                {
                    UserId = userId,
                    CreationDate = DateTime.UtcNow,
                    Status = Enum.Parse<Status>("Pending")
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            var orderItem = new OrderItem
            {
                ProductId = productId,
                Amount = amount,
                ProductPrice = product.Price,
                OrderId = order.Id
            };

            order.Items.Add(orderItem);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreationDate = order.CreationDate,
                IsPaid = order.IsPaid,
                Status = order.Status.ToString(),
                TotalPrice = order.TotalAmount,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Amount = oi.Amount,
                    ProductPrice = oi.ProductPrice
                }).ToList()
            };
        }

        public async Task<List<OrderDto>> GetOrdersAsync(Guid userId, int? orderId = null)
        {
            var query = _context.Orders
                .Include(o => o.Items)
                .Where(o => o.UserId == userId);

            if (orderId.HasValue)
            {
                query = query.Where(o => o.Id == orderId.Value);
            }

            var orders = await query.ToListAsync();

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreationDate = order.CreationDate,
                IsPaid = order.IsPaid,
                Status = order.Status.ToString(),
                TotalPrice = order.TotalAmount,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Amount = oi.Amount,
                    ProductPrice = oi.ProductPrice
                }).ToList()
            }).ToList();
        }

        public async Task<OrderDto> UpdateOrderAsync(Guid userId, OrderUpdateDto updateDto)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == updateDto.OrderId && o.Status == Status.Pending);

            if (order == null)
                throw new InvalidOperationException("Order not found or already paid.");

            foreach (var update in updateDto.Updates)
            {
                var orderItem = order.Items.FirstOrDefault(oi => oi.ProductId == update.ProductId);

                if (orderItem == null)
                    throw new InvalidOperationException($"Product with ID {update.ProductId} not found in the order.");

                if (update.NewAmount <= 0)
                {
                    _context.OrderItems.Remove(orderItem);
                }
                else
                {
                    orderItem.Amount = update.NewAmount;
                }
            }

            await _context.SaveChangesAsync();

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                CreationDate = order.CreationDate,
                IsPaid = order.IsPaid,
                Status = order.Status.ToString(),
                TotalPrice = order.TotalAmount,
                Items = order.Items.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    Amount = oi.Amount,
                    ProductPrice = oi.ProductPrice
                }).ToList()
            };
        }

        public async Task RemoveOrderItemsAsync(Guid userId, int orderId, List<int> itemIds)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId && o.Status == Status.Pending);

            if (order == null)
                throw new InvalidOperationException("Order not found or already paid.");

            var itemsToRemove = order.Items.Where(oi => itemIds.Contains(oi.Id)).ToList();

            if (!itemsToRemove.Any())
                throw new InvalidOperationException("No matching items found in the order.");

            _context.OrderItems.RemoveRange(itemsToRemove);

            await _context.SaveChangesAsync();
        }


        public async Task CompleteOrderAsync(Guid userId, int orderId)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.UserId == userId && o.Id == orderId && o.Status.ToString() == "Pending");

            if (order == null) throw new InvalidOperationException("Order not found or already paid");

            order.IsPaid = true;
            order.Status = Enum.Parse<Status>("Complete");
            await _context.SaveChangesAsync();
        }
    }
}
