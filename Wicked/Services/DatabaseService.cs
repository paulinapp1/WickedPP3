using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Wicked.Services
{
    public class DatabaseService
    {
        private readonly IMongoDatabase _database;
        public DatabaseService(IMongoClient mongoClient, IConfiguration configuration)
        {
            var databaseName = configuration.GetValue<string>("MongoDbSettings:DatabaseName");
            _database = mongoClient.GetDatabase(databaseName);
        }
        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
