using aspnet_core_mongo_api.Configurations;
using aspnet_core_mongo_api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace aspnet_core_mongo_api.Services
{
    public class DriverService
    {
        private readonly IMongoCollection<Driver> _driverCollection;

        public DriverService(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDb = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            _driverCollection = mongoDb.GetCollection<Driver>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<Driver>> GetAsync() => await _driverCollection.Find(_ => true).ToListAsync();
        public async Task<Driver> GetAsync(string id) => await _driverCollection.Find(driver => driver.Id == id).FirstOrDefaultAsync();
        public async Task CreateAsync(Driver driver) => await _driverCollection.InsertOneAsync(driver);
        public async Task UpdateAsync(Driver driver) => await _driverCollection.ReplaceOneAsync(x => x.Id == driver.Id, driver);
        public async Task DeleteAsync(string id) => await _driverCollection.DeleteOneAsync(driver => driver.Id == id);
    }
}
