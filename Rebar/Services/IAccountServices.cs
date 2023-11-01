using Rebar.Models;

namespace Rebar.Services
{
    public interface IAccountServices
    {
        Task<IEnumerable<Order>> GetAllOrders();      
        Task UpdateAccount(Guid id, Shake order);
    }
}
