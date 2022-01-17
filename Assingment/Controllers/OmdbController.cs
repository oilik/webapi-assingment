using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Assingment.Model;
using Assingment.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using OMDbApiNet;
using OMDbApiNet.Model;

namespace Assingment.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/omdb")]
    public class OmdbController : ControllerBase
    {
        private readonly IOmdbSearchRepository _omdbSearchRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<Settings> _config;
        static readonly Stopwatch timer = new Stopwatch();

        public OmdbController(IOmdbSearchRepository gameRepository, IHttpContextAccessor httpContextAccessor, IOptions<Settings> config)
        {
            _omdbSearchRepository = gameRepository;
            _httpContextAccessor = httpContextAccessor;
            _config = config;

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
          return new NotFoundResult();
        }


        [HttpGet("{title}", Name = "Get")]
        public async Task<IActionResult> Search(string title)
        {
            Stopwatch stopwatch = new Stopwatch();
            OmdbClient omdb = new OmdbClient(_config.Value.OmdbApiKey);
           

            try
            {
                stopwatch.Start();
                Item item = omdb.GetItemByTitle(title);

                var ts = stopwatch.Elapsed;

                var data = new OmdbSearch
                {
                    imdbID = item.ImdbId,
                    search_token = title,
                    ip_address = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString(),
                    processing_time_ms = ts.TotalMilliseconds,
                    timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()

                };
                await _omdbSearchRepository.Create(data); 
                return new ObjectResult(item);
                

               
            }
            catch (System.Exception ex) {

                return new NotFoundObjectResult(ex.Message);

            }finally { 

                stopwatch.Stop(); 
            }
            
  
         
        }

        
    }
}
