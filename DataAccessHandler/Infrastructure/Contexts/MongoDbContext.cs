using DataAccessHandler.Domain;
using MongoDB.Driver;
using System;

namespace DataAccessHandler.Infrastructure.Contexts
{
    public class MongoDbContext : IMongoDbContext
    {
        public string ConnectionString { get; private set; }

        public string DatabaseName { get; private set; }
        
        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            if (string.IsNullOrEmpty(ConnectionString))
                throw new ArgumentNullException(nameof(ConnectionString), "Cannot be null or empty !");
        }

        public MongoDbContext(string connectionString)
        {
            ConnectionString = connectionString;
            DatabaseName = MongoUrl.Create(ConnectionString).DatabaseName;
            var client = new MongoClient(ConnectionString);
            _database = client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<T> Collection<T>() where T : MongoEntity
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}
