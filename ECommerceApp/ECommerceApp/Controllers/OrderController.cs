using AutoMapper;
using ECommerceApp.Business.Contract.IRepository;
using ECommerceApp.Business.DTO.Order;
using ECommerceApp.Model.Request;
using ECommerceApp.Model.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerceApp.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a product to the user's order list. 
        /// </summary>
        /// <param name="request">The request model containing the product ID and amount to be added to the order.</param>
        /// <returns>A created response with the updated order list.</returns>
        /// <remarks>
        /// The product is added to the user's current "pending" order. If the user does not have an active order, a new order is created.
        /// The response includes the updated order list, reflecting the new product added.
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> AddProductToOrder([FromBody] OrderCreateRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orderDto = await _orderRepository.AddProductToOrderAsync(Guid.Parse(userId), request.ProductId, request.Amount);

            var orders = await _orderRepository.GetOrdersAsync(Guid.Parse(userId), null);

            var ordersResponse = _mapper.Map<List<OrderResponseModel>>(orders);

            return Ok(ordersResponse);
        }

        /// <summary>
        /// Retrieves a list of orders for the authenticated user.
        /// </summary>
        /// <param name="orderId">Optional parameter to specify a particular order by its ID. If omitted, all orders for the user will be returned.</param>
        /// <returns>A list of orders for the user, each represented by an `OrderResponseModel`.</returns>
        /// <remarks>
        [HttpGet]
        public async Task<IActionResult> GetOrder([FromQuery] int? orderId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var orders = await _orderRepository.GetOrdersAsync(Guid.Parse(userId), orderId);

            var ordersResponse = _mapper.Map<List<OrderResponseModel>>(orders);

            return Ok(ordersResponse);
        }

        /// <summary>
        /// Updates the details of a user's order, including the amount of products in the order.
        /// </summary>
        /// <param name="request">The request model containing the order ID and updates to be applied to the order items.</param>
        /// <returns>A response with the updated order list after the modification.</returns>
        /// <remarks>
        /// This method allows users to modify the amount of products in their order if the order is still in a "Pending" state. 
        /// After the order is updated, the response includes the user's updated order list with the modified details.
        /// </remarks>
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var updateDto = _mapper.Map<OrderUpdateDto>(request);

            var updatedOrder = await _orderRepository.UpdateOrderAsync(Guid.Parse(userId), updateDto);

            var orders = await _orderRepository.GetOrdersAsync(Guid.Parse(userId), null); 

            var ordersResponse = _mapper.Map<List<OrderResponseModel>>(orders);

            return Ok(ordersResponse);
        }

        /// <summary>
        /// Removes a product from the user's order list.
        /// </summary>
        /// <param name="request">The request model containing the order ID and the list of products to be removed from the order.</param>
        /// <returns>A no-content response indicating the product(s) were successfully removed.</returns>
        /// <remarks>
        /// This method allows users to remove specific products from their order. The removal will only be performed for "Pending" orders. 
        /// If the order has been paid, no products can be removed.
        /// </remarks>
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder([FromBody] OrderItemDeleteRequest request)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _orderRepository.RemoveOrderItemsAsync(Guid.Parse(userId), request.OrderId, request.ItemIds);
            return NoContent();
        }

        /// <summary>
        /// Finalizes the purchase for the products in the user's order, marking the order as paid.
        /// </summary>
        /// <returns>A no-content response indicating the order was successfully purchased.</returns>
        /// <remarks>
        /// This method processes the payment and changes the order's status to "Paid". It assumes the user has an active order with pending products.
        /// After purchasing, no further modifications can be made to the order.
        /// </remarks>
        [HttpPost("buy")]
        public async Task<IActionResult> CompleteOrder([FromQuery] int orderId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _orderRepository.CompleteOrderAsync(Guid.Parse(userId), orderId);
            return NoContent();
        }
    }
}
