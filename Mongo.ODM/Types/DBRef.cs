using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Mongo.ODM.Types;

public class DBRef<T> where T : BaseDocument
{

    [BsonIgnore]
    [JsonIgnore]
    private IMongoDatabase? _client { get; set; }
    [BsonIgnore]
    [JsonIgnore]
    private T? _value;
    public string CollectionName { get; set; }

    public DBRef(string CollectionName)
    {
        this.CollectionName = CollectionName;
    }

    public DBRef()
    {
        this.CollectionName = nameof(T);
    }

    internal void Inject(IMongoDatabase client)
    {
        _client= client;
    }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? _Id { get; set; }


    public void Reload()
    {
        ReloadAsync().Wait();
    }

    public async Task ReloadAsync()
    {
        if(_client == null) return;
        var docs = await _client.GetCollection<T>(CollectionName).FindAsync(i => i.Id == _Id);
        _value = await docs.FirstOrDefaultAsync();
    }

    public async Task<T?> ValueAsync() {
        if(_value == null)
            await ReloadAsync();
        return _value;
    }



    [BsonIgnore]
    public T? Value { get {
        if(_value == null)
            Reload();
        return _value;
    } 
    set {
        _Id = value?.Id ?? null;
        _value = value;
    } }

}
