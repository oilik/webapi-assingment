using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Assingment.Model;
using Assingment.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using OMDbApiNet;
using OMDbApiNet.Model;

namespace Assingment.Controllers
{
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IOmdbSearchRepository _omdbSearchRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<Settings> _config;


        public AdminController(IOmdbSearchRepository gameRepository, IHttpContextAccessor httpContextAccessor, IOptions<Settings> config)
        {
            _omdbSearchRepository = gameRepository;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var token = HttpContext.GetTokenAsync("access_token").Result;
            //return new ObjectResult(token);
            return new ObjectResult(await _omdbSearchRepository.GetAllSearches());
        }

    }
}
