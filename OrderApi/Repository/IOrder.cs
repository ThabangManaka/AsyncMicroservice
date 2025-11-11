using Shared.DTOs;
using Shared.Models;

namespace OrderApi.Repository
{
    public interface IOrder
    {
        Task<ServiceResponse> AddOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();
        Task<OrderSummary> GetOrderSummaryAsync();  

    }
}
