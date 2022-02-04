using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using MongoDB.Driver;

namespace Mongo.ODM;

public class ODMClient
{

    public IMongoClient MongoClient { get; init; }

    public ODMClient(IMongoClient client) {
        MongoClient = client;
    }

    public ODMClient(string connectionString) {
        MongoClient = new MongoClient(connectionString);
    }

    public ODMDatabase GetDatabase(string name, MongoDatabaseSettings settings = null)
    {
        var baseClient = MongoClient.GetDatabase(name, settings);
        return new ODMDatabase(baseClient);
    }


}
