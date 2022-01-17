using Assingment.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assingment.Repository
{
    public class OmdbSearchRepository : IOmdbSearchRepository
    {
        private readonly IDBContext _context;

        public OmdbSearchRepository(IDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OmdbSearch>> GetAllSearches()
        {
            return await _context
                            .OmdbSearches
                            .Find(_ => true)
                            .ToListAsync();
        }

        public async Task Create(OmdbSearch search)
        {
            await _context.OmdbSearches.InsertOneAsync(search);
        }



    }
}
