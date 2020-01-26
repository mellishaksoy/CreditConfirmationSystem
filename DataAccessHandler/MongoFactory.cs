using DataAccessHandler.Infrastructure.Contexts;
using MongoDB.Driver;

namespace DataAccessHandler
{
    public class MongoFactory : IMongoFactory
    {
        private readonly IMongoDbContext _mongoDbContext;

        public MongoFactory(IMongoDbContext mongoDbContext)
        {
            _mongoDbContext = mongoDbContext;
        }
        
    }
}
