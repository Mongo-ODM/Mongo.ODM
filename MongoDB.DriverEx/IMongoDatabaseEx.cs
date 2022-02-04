using MongoDB.Driver;

namespace MongoDB.DriverEx;

public static class IMongoDatabaseEx
{

    public static IMongoCollection<T> GetCollection<T>(this IMongoDatabase client, MongoCollectionSettings settings = null)
    {
        return client.GetCollection<T>(nameof(T), settings);
    }
    public static void CreateCollection<T>(this IMongoDatabase client, CreateCollectionOptions options = null, CancellationToken cancellationToken = default)
    {
        client.CreateCollection(nameof(T), options, cancellationToken);
    }

    public static Task CreateCollectionAsync<T>(this IMongoDatabase client, CreateCollectionOptions options = null, CancellationToken cancellationToken = default)
    {
        return client.CreateCollectionAsync(nameof(T), options, cancellationToken);
    }


}
