using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Assingment.Model
{
    public class DBContext : IDBContext
    {
        private readonly IMongoDatabase _db;

        public DBContext(IOptions<Settings> options, IMongoClient client)
        {
            _db = client.GetDatabase(options.Value.Database);

        }

        public IMongoCollection<OmdbSearch> OmdbSearches => _db.GetCollection<OmdbSearch>("OmdbSearches");
      
    }
}
