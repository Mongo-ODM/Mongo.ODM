using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo.ODM;

public class ODMDatabase
{
    public IMongoDatabase MongoDatabase { get; init; }

    internal ODMDatabase(IMongoDatabase baseClient)
    {
        MongoDatabase = baseClient;
    }

    public ODMCollection<TDocument> GetCollectionAsync<TDocument>(MongoCollectionSettings settings = null) where TDocument : BaseDocument
    {
        return GetCollection<TDocument>(nameof(TDocument), settings);
    }

    public ODMCollection<TDocument> GetCollection<TDocument>(string name, MongoCollectionSettings settings = null) where TDocument : BaseDocument
    {
        return new ODMCollection<TDocument>(MongoDatabase.GetCollection<TDocument>(name, settings), MongoDatabase);
    }

}
