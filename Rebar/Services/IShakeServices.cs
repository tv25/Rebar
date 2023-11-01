using Rebar.Models;

namespace Rebar.Services
{
    public interface IShakeServices
    {
        Task<IEnumerable<Shake>> GetAllShakes();
        Task<Shake> GetShakeById(Guid id);
        Task AddShake(Shake order);
        Task UpdateShake(Guid id, Shake order);
    }
}
