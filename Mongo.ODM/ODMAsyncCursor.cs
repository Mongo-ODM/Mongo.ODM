using MongoDB.Driver;
using System.Linq;
namespace Mongo.ODM;

public class ODMAsyncCursor<TDocument> : IAsyncCursor<TDocument> where TDocument : BaseDocument
{
    IAsyncCursor<TDocument> baseCursor;
    ODMCollection<TDocument> collection;
    internal ODMAsyncCursor(IAsyncCursor<TDocument> baseCursor, ODMCollection<TDocument> collection)
    {
        this.baseCursor = baseCursor;
        this.collection = collection;
    }

    IEnumerable<TDocument>? Last;
    IEnumerable<TDocument>? _Current;
    public IEnumerable<TDocument> Current { get {
        if(Last == baseCursor.Current && _Current != null)
            return _Current;
        _Current = baseCursor.Current.Select(i => {
            i.Inject(collection.MongoDatabase);
            return i;
        });
        Last = baseCursor.Current;
        return _Current;
    } }

    public void Dispose()
    {
        baseCursor.Dispose();
    }

    public bool MoveNext(CancellationToken cancellationToken = default)
    {
        return baseCursor.MoveNext(cancellationToken);
    }

    public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default)
    {
        return baseCursor.MoveNextAsync(cancellationToken);
    }
}
