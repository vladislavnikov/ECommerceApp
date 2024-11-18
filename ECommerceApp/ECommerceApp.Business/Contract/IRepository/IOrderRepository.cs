using ECommerceApp.Business.DTO.Order;

namespace ECommerceApp.Business.Contract.IRepository
{
    public interface IOrderRepository
    {
        Task<OrderDto> AddProductToOrderAsync(Guid userId, int productId, int amount);
        Task<List<OrderDto>> GetOrdersAsync(Guid userId, int? orderId = null);
        Task<OrderDto> UpdateOrderAsync(Guid userId, OrderUpdateDto updateDto);
        Task RemoveOrderItemsAsync(Guid userId, int orderId, List<int> itemIds);
        Task CompleteOrderAsync(Guid userId, int orderId);
    }
}
