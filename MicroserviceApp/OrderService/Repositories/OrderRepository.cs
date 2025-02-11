using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OrderService.Models;
namespace OrderService.Repositories
{
        public class OrderRepository : IOrderRepository
        {
        private readonly IMongoCollection<Order> _collection;

        public OrderRepository(
            IOptions<MongoDbSettings> mongoDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                mongoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<Order>("orders");
        }


        public async Task AddAsync(Order order)
        {
            await _collection.InsertOneAsync(order);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }


        public async Task<Order?> GetByIdAsync(string id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _collection.ReplaceOneAsync(x => x.Id == order.Id, order);
        }
    }
    }
