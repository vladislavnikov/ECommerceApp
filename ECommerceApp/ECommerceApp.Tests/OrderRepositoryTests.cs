using System;
using ECommerceApp.Business.DTO.Order;
using ECommerceApp.DAL.Data.Models.Enum;
using ECommerceApp.Business.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ECommerceApp.Tests.Repository
{
    public class OrderRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly OrderRepository _orderRepository;
        private readonly Guid _userId;

        public OrderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())  
                .Options;

            _context = new ApplicationDbContext(options);
            _orderRepository = new OrderRepository(_context);
            _userId = Guid.NewGuid();  
        }

        [Fact]
        public async Task AddProductToOrderAsync_CreatesNewOrder_WhenNoPendingOrderExists()
        {
            var productId = 1;
            var amount = 2;

            var orderDto = await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderDto.Id);

            Assert.NotNull(order);
            Assert.Equal(_userId, order.UserId);
            Assert.Equal(Status.Pending, order.Status);
            Assert.Single(order.Items);
            Assert.Equal(productId, order.Items.First().ProductId);
            Assert.Equal(amount, order.Items.First().Amount);
        }

        [Fact]
        public async Task GetOrdersAsync_ReturnsOrders_WhenOrderIdIsNotProvided()
        {
            var productId = 1;
            var amount = 2;
            await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            var orders = await _orderRepository.GetOrdersAsync(_userId);

            Assert.NotEmpty(orders);
            Assert.Equal(_userId, orders.First().UserId);
        }

        [Fact]
        public async Task GetOrdersAsync_ReturnsSingleOrder_WhenOrderIdIsProvided()
        {
            var productId = 1;
            var amount = 2;
            var orderDto = await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            var orders = await _orderRepository.GetOrdersAsync(_userId, orderDto.Id);

            Assert.Single(orders);
            Assert.Equal(orderDto.Id, orders.First().Id);
        }

        [Fact]
        public async Task UpdateOrderAsync_UpdatesProductAmount_WhenProductExists()
        {
            var productId = 1;
            var amount = 2;
            var orderDto = await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            var updateDto = new OrderUpdateDto
            {
                OrderId = orderDto.Id,
                Updates = new List<OrderItemUpdateDto>
                {
                    new OrderItemUpdateDto { ProductId = productId, NewAmount = 5 }
                }
            };

            var updatedOrder = await _orderRepository.UpdateOrderAsync(_userId, updateDto);

            var updatedItem = updatedOrder.Items.First();
            Assert.Equal(5, updatedItem.Amount);
        }

        [Fact]
        public async Task UpdateOrderAsync_RemovesProduct_WhenAmountIsZero()
        {
            var productId = 1;
            var amount = 2;
            var orderDto = await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            var updateDto = new OrderUpdateDto
            {
                OrderId = orderDto.Id,
                Updates = new List<OrderItemUpdateDto>
                {
                    new OrderItemUpdateDto { ProductId = productId, NewAmount = 0 }
                }
            };

            var updatedOrder = await _orderRepository.UpdateOrderAsync(_userId, updateDto);

            Assert.Empty(updatedOrder.Items); 
        }

        [Fact]
        public async Task RemoveOrderItemsAsync_RemovesItemsFromOrder()
        {
            var productId = 1;
            var amount = 2;
            var orderDto = await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            var itemIds = orderDto.Items.Select(i => i.Id).ToList();
            await _orderRepository.RemoveOrderItemsAsync(_userId, orderDto.Id, itemIds);

            var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == orderDto.Id);

            Assert.Empty(order.Items);  
        }

        [Fact]
        public async Task CompleteOrderAsync_ChangesOrderStatusToCompleted()
        {
            var productId = 1;
            var amount = 2;
            var orderDto = await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            await _orderRepository.CompleteOrderAsync(_userId, orderDto.Id);

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderDto.Id);

            Assert.Equal(Status.Completed, order.Status);
            Assert.True(order.IsPaid);
        }

        [Fact]
        public async Task CompleteOrderAsync_ThrowsException_WhenOrderIsAlreadyCompleted()
        {
            var productId = 1;
            var amount = 2;
            var orderDto = await _orderRepository.AddProductToOrderAsync(_userId, productId, amount);

            await _orderRepository.CompleteOrderAsync(_userId, orderDto.Id);

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _orderRepository.CompleteOrderAsync(_userId, orderDto.Id));

            Assert.Equal("Order not found or already paid", exception.Message);
        }
    }
}
