using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Rebar.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;


namespace Rebar.Services
{

    public class AccountServices : IAccountServices
    {
        private OrderServices _OrderServices;
        public async Task<IEnumerable<Order>> GetAllOrders ()=>
            await _OrderServices.GetAllOrders();

        
        public Task UpdateAccount(Guid id, Shake order)
        {
            throw new NotImplementedException();
        }
    }
}
