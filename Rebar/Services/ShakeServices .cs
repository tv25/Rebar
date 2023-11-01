using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Rebar.Models;

namespace Rebar.Services
{
    public class ShakeServices : IShakeServices
    {
        private readonly IMongoCollection<Shake> _ShakeCollection;
        private readonly IOptions<DataBaseSettings> _dbSettings;
        
        public ShakeServices(IOptions<DataBaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var MongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var MongoDatabase = MongoClient.GetDatabase(dbSettings.Value.DataBaseName);
            _ShakeCollection = MongoDatabase.GetCollection<Shake>
                (dbSettings.Value.ShakeCollectionName);
        }
        public async Task<IEnumerable<Shake>> GetAllShakes() =>
            await _ShakeCollection.Find(_ => true).ToListAsync();

        public async Task<Shake> GetShakeById(Guid id) =>
            await _ShakeCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task AddShake(Shake shake) =>
            await _ShakeCollection.InsertOneAsync(shake);

        public async Task UpdateShake(Guid id, Shake shake) =>
            await _ShakeCollection
            .ReplaceOneAsync(a => a.Id == id, shake);

       
    }
}
