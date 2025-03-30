using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IOrderService
    {
        Task CreateOrderAsync(CreateOrder createOrder);
        Task<ListOrder> GetAllOrdersAsync(int page, int size);
        Task<SingleOrder> GetOrderByIdAsync(string id);
        Task<Order> GetRealOrderByIdAsync(string id);
        Task<(bool, CompletedOrderDTO)> CompleteOrderAsync(string id);
    }
}
