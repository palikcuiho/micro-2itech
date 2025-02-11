using OrderService.DTOs;
using OrderService.Models;

namespace OrderService.Services
{
    public interface IOrdersService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(string id);
        Task<OrderDTO?> AddOrderAsync(OrderPostDTO order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(string id);


    }
}
