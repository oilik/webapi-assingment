using Assingment.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assingment.Repository
{
    public interface IOmdbSearchRepository
    {
        Task<IEnumerable<OmdbSearch>> GetAllSearches();
        Task Create(OmdbSearch search);
        //Task<OmdbSearchRepository> GetSearch(string token);
        //
        //Task<bool> Update(OmdbSearchRepository search);
        //Task<bool> Delete(string token);

    }
}
