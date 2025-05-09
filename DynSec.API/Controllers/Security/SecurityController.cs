﻿
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;


namespace DynSec.API.Controllers.Security
{

    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : Controller
    {
        //public const string CorsPolicyName = "Bff";

        [HttpGet("check-session")]
        //[EnableCors(CorsPolicyName)]
        public ActionResult<IDictionary<string, string>> CheckSession()
        {
            // return 401 Unauthorized to force SPA redirection to Login endpoint
            if (User.Identity?.IsAuthenticated != true)
                return Unauthorized();

            return User.Claims.ToDictionary(claim => claim.Type, claim => claim.Value);
        }

        [HttpGet("login")]
        public ActionResult<IDictionary<string, string>> Login([FromQuery] string? returnUrl)
        {
            // Logic to initiate the authorization code flow
            return Challenge(new AuthenticationProperties { RedirectUri = Url.Content(returnUrl ?? "~/") });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // Logic to handle logging out the user
            return SignOut();
        }
    }
}


