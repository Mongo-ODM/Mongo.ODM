using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Mongo.ODM;

public partial class BaseDocument
{

    internal void Inject(IMongoDatabase mongoDatabase)
    {
        #if SG
            _Inject(mongoDatabase);
        #else

        #endif
    }

    [BsonIgnore]
    [JsonIgnore]
    internal BaseDocument CleanDocument; 

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonIgnore]
    [JsonIgnore]
    public bool IsDirty { get {
        #if SG
            return _IsDirty();
        #else
            return true;
        #endif
    } }

    

}
