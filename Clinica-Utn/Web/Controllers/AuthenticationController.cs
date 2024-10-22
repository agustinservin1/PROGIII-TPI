using Application.Interfaces;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticateService;
        public AuthenticationController(IAuthenticationService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [HttpPost("[action]")]
        public IActionResult AuthenticateUser([FromBody] CredentialRequest request)
        {
            string token = _authenticateService.AuthenticateCredentials(request);

            if (token is not null)
            {
                return (Ok(token));
            }

            return Unauthorized();
        }
    }
}
