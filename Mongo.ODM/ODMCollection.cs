using MongoDB.Driver;

namespace Mongo.ODM;

public class ODMCollection<TDocument>
{

    public IMongoCollection<TDocument> MongoCollection { get; init; }
    internal IMongoDatabase MongoDatabase { get; init; }

    internal ODMCollection(IMongoCollection<TDocument> baseCollection, IMongoDatabase mongoDatabase)
    {
        MongoDatabase = mongoDatabase;
        MongoCollection = baseCollection;
    }

}
