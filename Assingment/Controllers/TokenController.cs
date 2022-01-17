using Assingment.Model;
using Assingment.TokenManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Assingment.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IJwtTokenManager _tokenManager;

        public TokenController(IJwtTokenManager jwtTokenManager)
        {
            _tokenManager = jwtTokenManager;

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenicate(UserCredential credential) {
            var token = _tokenManager.Authenticate(credential.UserName, credential.Password);

            if (string.IsNullOrWhiteSpace(token))  
                return Unauthorized();

            return Ok(token);        
        }
    }
}
