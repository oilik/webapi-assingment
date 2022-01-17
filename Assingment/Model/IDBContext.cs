using MongoDB.Driver;

namespace Assingment.Model
{
    public interface IDBContext
    {
        IMongoCollection<OmdbSearch> OmdbSearches { get; }
    }
}
