using Microsoft.AspNetCore.Mvc;
using Rebar.Models;

namespace Rebar.Services
{
    public interface IOrderServices
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> GetOrderById(Guid id);
        Task AddOrder(Order order);
        Task UpdateOrder(Guid id, Order order);
        Task<Shake> ManageMenu();
        Task<List<Shake>> GetShakesInOrder();
        Task<Shakes> GetSizeFoShake(Shake shake);
    }
}
