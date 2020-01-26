using DataAccessHandler.Domain;
using MongoDB.Driver;

namespace DataAccessHandler.Infrastructure.Contexts
{
    public interface IMongoDbContext
    {
        string ConnectionString { get; }
        string DatabaseName { get; }
        IMongoCollection<T> Collection<T>() where T : MongoEntity;
    }
}
